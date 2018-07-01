using Slacker.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Slacker {
    public class DataModelField {

        public bool IsKeyField { get; protected set; }
        public bool IsPrimaryKeyField { get; protected set; }
        public bool IsIgnored { get; protected set; }

        public string TableField { get; protected set; }
        public string ModelField { get; protected set; }
        public Type ModelFieldType { get; protected set; }
        public Field FieldAttribute { get; protected set; }

        public DataModelField(FieldInfo field) {
            FieldAttribute = field.GetCustomAttribute<Field>();
            if (FieldAttribute == null) {
                TableField = ModelField = field.Name;
                return;
            }

            if (FieldAttribute.Ignored) {
                IsIgnored = true;
                return;
            }
            
            // Load Properties
            IsPrimaryKeyField = FieldAttribute.IsPrimary;
            TableField = FieldAttribute.Name ?? field.Name;
            ModelField = field.Name;
        }

    }
}
