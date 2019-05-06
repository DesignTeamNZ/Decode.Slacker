using System;
using System.Collections.Generic;
using System.Text;

namespace Slacker.Interfaces {
    public interface IDataFieldDefinition {
        string FieldName { get; set; }
        string BindingPropName { get; set; }
        string FieldNameAsSql { get; }
        FieldType FieldType { get; }
        int Length { get; }
        KeyType KeyType { get; }

    }
}
