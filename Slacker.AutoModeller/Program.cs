using Slacker.AutoModeller.Helpers;
using Slacker.AutoModeller.Models;
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

        static string EXPORT_SYNTAX = "sam export <connectionStringConfigId> <catalog> <outputDir>";

        static void Main(string[] args) {
            Console.BackgroundColor = ConsoleColor.Blue;
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
                WhereSql = "TABLE_CATALOG = @Catalog",
                WhereParams = new {
                    Catalog = args[2]
                }
            });

            // Get Output Directory
            var outputDirectory = !Path.IsPathRooted(args[3]) ?
                Path.Combine(Environment.CurrentDirectory, args[3]) :
                args[3];



            Console.Read();
        }
    }
}
