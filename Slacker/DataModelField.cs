using Slacker.Helpers.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Slacker {
    public class DataModelField {
        
        public string TableField { get; protected set; }
        public string ModelField { get; protected set; }

        public Type ModelFieldType { get; protected set; }
        public FieldAttribute FieldAttribute { get; protected set; }
        
        public bool IsPrimary {
            get {
                return FieldAttribute?.IsPrimary ?? false;
            }
        }
        public bool IsIgnored {
            get {
                return FieldAttribute?.Ignored ?? false;
            }
        }
        public bool IsGenerated {
            get {
                return FieldAttribute?.IsGenerated ?? false;
            }
        }

        public DataModelField(MemberInfo memberField) {
            FieldAttribute = memberField.GetCustomAttribute<FieldAttribute>();
            if (FieldAttribute == null) {
                TableField = ModelField = memberField.Name;
                return;
            }
            
            // Load Properties
            TableField = FieldAttribute.Name ?? memberField.Name;
            ModelField = memberField.Name;
        }

    }
}
