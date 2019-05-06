using Slacker.Interfaces;
using System;
using System.Collections.Generic;

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
    public class SlackerIgnoreAttribute : System.Attribute { }

    public enum KeyType {
        NONE = 0,
        PRIMARY_KEY = 1,
        GENERATED_KEY = 2
    }

    public enum FieldType {
        NOT_SPECIFIED = 0,
        // Exact numerics
        BIG_INT,   
        NUMERIC, 
        BIT,
        SMALL_INT, 
        DECIMAL,   
        SMALL_MONEY, 
        INT,
        TINY_INT,
        MONEY,
        // Approximate numerics
        FLOAT,
        REAL,
        // Date and time
        DATE = 10,
        DATETIMEOFFSET = 11,
        DATETIME = 12,
        DATETIME2 = 13,
        SMALL_DATETIME = 14,
        TIME = 15,
        // Character strings
        CHAR = 20,
        VARCHAR = 21,
        TEXT = 22,
        NCHAR = 23,
        NVARCHAR = 24,
        NTEXT = 25,
        // BINARY
        BINARY = 30,
        VARBIBNARY = 31,
        IMAGE = 32,
        // MISC    
        CURSOR = 40,
        ROW_VERSION = 41,
        HIERARCHY_ID = 42,
        UNIQUE_IDENTIFIER = 44,
        SQL_VARIANT = 45,
        XML = 46,
        GEOGRAPHY = 47,
        TABLE = 48,
        JSON = 49

    }

    public static class FieldTypeExtensions {

        readonly static Dictionary<FieldType, Type> TYPE_MAPPING = new Dictionary<FieldType, Type>() {
            { FieldType.BIG_INT,    typeof(long) },
            { FieldType.NUMERIC,    typeof(decimal) },
            { FieldType.BIT,        typeof(bool) },
            { FieldType.SMALL_INT,  typeof(short) },
            { FieldType.DECIMAL,    typeof(decimal) },
            { FieldType.SMALL_MONEY, typeof(decimal) },
            { FieldType.INT,        typeof(int) },
            { FieldType.TINY_INT,   typeof(int) },
            { FieldType.MONEY,      typeof(decimal) },

            { FieldType.FLOAT,      typeof(float) },
            { FieldType.REAL,       typeof(Single) },

            { FieldType.DATE,       typeof(DateTime) },
            { FieldType.DATETIMEOFFSET, typeof(DateTime) },
            { FieldType.DATETIME,   typeof(DateTime) },
            { FieldType.DATETIME2,  typeof(DateTime) },
            { FieldType.SMALL_DATETIME, typeof(DateTime) },
            { FieldType.TIME,       typeof(DateTime) },

            { FieldType.CHAR,       typeof(string) },
            { FieldType.VARCHAR,    typeof(string) },
            { FieldType.TEXT,       typeof(string) },
            { FieldType.NCHAR,      typeof(string) },
            { FieldType.NVARCHAR,   typeof(string) },
            { FieldType.NTEXT,      typeof(string) },

            { FieldType.BINARY,     typeof(Byte[]) },
            { FieldType.VARBIBNARY, typeof(Byte[]) },
            { FieldType.IMAGE,      typeof(Byte[]) },

            // Honestly, I eyeballed the following mapping, Feel free to pull request
            { FieldType.CURSOR,     typeof(string) },
            { FieldType.ROW_VERSION, typeof(string) },
            { FieldType.HIERARCHY_ID, typeof(string) },
            { FieldType.UNIQUE_IDENTIFIER, typeof(string) },
            { FieldType.SQL_VARIANT, typeof(string) },
            { FieldType.XML,        typeof(string) },
            { FieldType.GEOGRAPHY,  typeof(string) },
            { FieldType.TABLE,      typeof(string) },
            { FieldType.JSON,       typeof(string) },
            
        };

        public static Type GetCodeEquivalentType(this FieldType type, bool nullable = false) {
            if (!TYPE_MAPPING.TryGetValue(type, out Type mappedType)) {
                return typeof(string);
            }

            return nullable ? GetNullableType(mappedType) : mappedType;
        }

        static Type GetNullableType(Type type) {
            // Use Nullable.GetUnderlyingType() to remove the Nullable<T> wrapper if type is already nullable.
            type = Nullable.GetUnderlyingType(type) ?? type; // avoid type becoming null
            return type.IsValueType ?
                typeof(Nullable<>).MakeGenericType(type) : type;
        }
    }
}
