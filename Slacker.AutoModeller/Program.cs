using Slacker.AutoModeller.Helpers;
using Slacker.AutoModeller.Models;
using Slacker.AutoModeller.Templates;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.AutoModeller {
    public class Program {

        static readonly string EXPORT_SYNTAX = "sam export <connectionStringConfigId> <catalog> <outputDir>";

        static void Main(string[] args) {
            Console.WriteLine(ConsoleHelpers.GetSlackerHeaderText());

            if(args.Length < 4 || args[0].ToLower() != "export") {
                Console.WriteLine("Invalid command syntax: " + EXPORT_SYNTAX);

                Console.Read();
                return;
            }

            // Get Connection String
            var connectionString = ConfigurationManager.ConnectionStrings
                .Cast<ConnectionStringSettings>().FirstOrDefault(
                    connStr => connStr.Name == args[1]
                )
            ?.ConnectionString;


            // Initialize DataService
            var dataService = new GenericDataService<InformationSchemaColumn>(
                SqlConnectionService.FromConnectionString(connectionString)
            );

            // Get Information Schema
            var informationSchema = dataService.Select(new QueryProps() {
                WhereSql = "TABLE_CATALOG LIKE @Catalog",
                WhereParams = new {
                    Catalog = args[2]
                }
            });

            if (!informationSchema.Any()) {
                Console.WriteLine("Could not find any results matching information schema target.");
                Console.Read();
                return;
            }

            // Get Output Directory
            var catalogName = DataModelTemplate.GetPropertyName(informationSchema.First().TableCatalog);
            var catalogDir = !Path.IsPathRooted(args[3]) ?
                Path.Combine(Environment.CurrentDirectory, args[3], catalogName) :
                Path.Combine(args[3], catalogName);
            
            // Create Catalog Directory if not exists
            if (!Directory.Exists(catalogDir)) {
                Directory.CreateDirectory(catalogDir);
            }

            // Create output schema delegate
            void OutputSchema(IGrouping<string, InformationSchemaColumn> schemaGroup) {
                var schemaName = DataModelTemplate.GetPropertyName(schemaGroup.First().TableSchema);
                var schemaDir = Path.Combine(catalogDir, schemaName);

                // Create Schema Directory if not exists
                if (!Directory.Exists(schemaDir)) {
                    Directory.CreateDirectory(schemaDir);
                }

                // Create output file delegate
                void OutputFile(DataModelTemplate dataModelTemplate) {
                    var tableName = DataModelTemplate.GetPropertyName(dataModelTemplate.TableName);
                    var filePath = Path.Combine(schemaDir, tableName + ".cs");

                    if (File.Exists(filePath)) {
                        File.Delete(filePath);
                    }

                    Console.WriteLine($"Generating Model: {catalogName}\\{schemaName}\\{tableName}.cs" );
                    File.WriteAllText(filePath, dataModelTemplate.TransformText());
                }

                // Output files
                schemaGroup.GroupBy(
                    infoSchemaCol => infoSchemaCol.TableName
                ).Select(
                    infoSchemaCols => new DataModelTemplate(infoSchemaCols.ToList())
                ).ToList().ForEach(
                    OutputFile
                );
            }

            // Build Models
            informationSchema.GroupBy(
                infoSchemaCol => infoSchemaCol.TableSchema
            ).ToList().ForEach(OutputSchema);


            Console.WriteLine("Done");
            Console.Read();
        }
    }
}
