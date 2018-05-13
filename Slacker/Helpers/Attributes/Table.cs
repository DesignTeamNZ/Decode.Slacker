using System;

namespace Slacker.Helpers.Attributes{

    /// <summary>
    /// Attribute used on DatabModels to override default table name generation
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class Table : System.Attribute {
        public string Name { get; set; }
        public Table(string name) { this.Name = name; }
    }
}
