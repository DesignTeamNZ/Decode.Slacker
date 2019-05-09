using Slacker.AutoModeller.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Slacker.AutoModeller.Templates {
    public partial class DataModelTemplate {
        
        static readonly string GROUP_TEXT_EXPR = "((?:\\d+)|(?:[A-Z][a-z]+)|(?:[A-Z]+)|(?:[a-z]+))";

        public List<InformationSchemaColumn> InfoSchema { get; protected set; }

        public string Namespace { get; protected set; }
        public string TableName { get; protected set; }
        public string Alias { get; protected set; }

        public DataModelTemplate(List<InformationSchemaColumn> infoSchema) {
            this.InfoSchema = infoSchema;

            var first = infoSchema.First();
            string safeCatalogName = GetPropertyName(first.TableCatalog);
            string safeSchemaName = GetPropertyName(first.TableSchema);
            string safeTableName = GetPropertyName(first.TableName);


            this.Namespace = $"{safeCatalogName}.{safeSchemaName}";
            this.TableName = first.TableName;
            this.Alias =
                    (safeSchemaName.Length > 5 ? safeSchemaName.Substring(0, 5) : safeSchemaName)
                  + (safeTableName.Length > 5 ? safeTableName.Substring(0, 5) : safeTableName);
        }

        /// <summary>
        /// Converts a line of text into a PropertyName
        /// Example:    TEST_PILOT     => TestPilot
        ///             testPilot      => TestPilot
        ///             Test Pilot     => TestPilot
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public static string GetPropertyName(string text) {
            var matches = Regex.Matches(text, GROUP_TEXT_EXPR);
            if (matches.Count == 0) {
                return "Field";
            }

            // Build name components from text
            var nameComponenets = matches.Cast<Match>().Select(
                match => match.Groups[0].Value
            ).Select(
                matchText => matchText.Substring(0, 1).ToUpper()
                    + matchText.Substring(1).ToLower()
            );

            var propName = string.Join("", nameComponenets);
            // Return name with "_" if number
            return Regex.Matches(propName, "^\\d").Count == 0 ? propName : "_" + propName;
        }

        public static FieldType GetFieldType(string fromName) {
            return Enum.GetValues(typeof(FieldType)).Cast<FieldType>().FirstOrDefault(
                field => field.ToString().Replace("_", "").ToLower() 
                            == fromName.Replace("_", "").ToLower()
            );
        }

        public static string GetTypeAsSourceDefinition(Type type) {
            var underlayingNullType = Nullable.GetUnderlyingType(type);
            if (underlayingNullType == null) {
                return type.Name;
            }
            
            return underlayingNullType.Name + "?";
        }
        
    }
}
