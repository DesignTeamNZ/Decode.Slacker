using System;

namespace Slacker.Helpers.Attributes
{
    /// <summary>
    /// Attribute used in DataModels to override default mapping
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property)]
    public class Field : System.Attribute {
        public string Name { get; set; }
        public bool IsPrimary { get; set; }
        public bool Ignored { get; set; }
    }
}
