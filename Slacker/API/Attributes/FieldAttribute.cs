using Slacker.Interfaces;
using System;

namespace Slacker {

    /// <summary>
    /// Attribute used in DataModels to override default mapping
    /// </summary>
    [System.AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class FieldAttribute : System.Attribute, IDataFieldDefinition {
        private string _bindingPropName;
        private string _fieldName;
        private string _fieldNameAsSql;

        public string BindingPropName {
            get => _bindingPropName;
            set {
                if (FieldName == null) {
                    FieldName = value;
                }
                _bindingPropName = value;
            }
        }

        public string FieldName {
            get => _fieldName;
            set {
                _fieldName = value;
                _fieldNameAsSql = $"[{value.Replace(".", "].[")}]";
            }
        }

        public string FieldNameAsSql {
            get => _fieldNameAsSql;
        }

        public FieldType FieldType { get; protected set; }
        public int Length { get; protected set; }
        public KeyType KeyType { get; protected set; }


        [Obsolete] public bool IsPrimary { get => KeyType == KeyType.PRIMARY_KEY; }
        [Obsolete] public bool IsGenerated { get => KeyType != KeyType.NONE; }

        public FieldAttribute(
            string name = null, 
            FieldType fieldType = default(FieldType), 
            int length = 0, 
            KeyType keyType = default(KeyType)
        ) {

            this.FieldName = name;
            this.FieldType = fieldType;
            this.Length = length;
            this.KeyType = keyType;
        }
    }

    public class GenericDataFieldDefinition : IDataFieldDefinition {
        private string _bindingPropName;
        private string _fieldName;
        private string _fieldNameAsSql;
        
        public string BindingPropName {
            get => _bindingPropName;
            set {
                if (FieldName == null) {
                    FieldName = value;
                }
                _bindingPropName = value;
            }
        }

        public string FieldName {
            get => _fieldName;
            set {
                _fieldName = value;
                _fieldNameAsSql = $"[{value.Replace(".", "].[")}]";
            }
        }
        public string FieldNameAsSql {
            get => _fieldNameAsSql;
        }

        public FieldType FieldType { get; protected set; }
        public int Length { get; protected set; }
        public KeyType KeyType { get; protected set; }
    }


    [System.AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class SlackerIgnoreAttribute : System.Attribute {}

    public enum KeyType {
        NONE = 0,
        PRIMARY_KEY = 1,
        GENERATED_KEY = 2
    }

    public enum FieldType {
        NOT_SPECIFIED = 0,
        // Numeric Data Types
        BIT         = 1,
        TINY_INT    = 2,
        SMALL_INT   = 3,
        INT         = 4,
        BIG_INT     = 5,
        DECIMAL     = 6,
        NUMERIC     = 7,
        FLOAT       = 8,
        REAL        = 9,
        // Date and Time Data Types
        DATE        = 10,
        TIME        = 11,
        DATETIME    = 12,
        TIMESTAMP   = 13,
        YEAR        = 14,
        // String Data
        CHAR        = 20,
        VARCHAR     = 21,
        TEXT        = 22,
        NCHAR       = 23,
        NVARCHAR    = 24,
        NTEXT       = 25,
        // BINARY
        BINARY      = 30,
        VARBIBNARY  = 31,
        IMAGE       = 32,
        // MISC    
        CLOB        = 40,
        BLOB        = 41,
        XML         = 42,
        JSON        = 43,
        UNIQUE_IDENTIFIER = 44,
        GEOGRAPHY         = 45

    }
}
