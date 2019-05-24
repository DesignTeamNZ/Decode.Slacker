using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slacker.AutoModeller.Models {

    [Table("INFORMATION_SCHEMA.COLUMNS", "isc")]
    public partial class InformationSchemaColumn : DataModel {

        [Field("TABLE_CATALOG", FieldType.NVARCHAR, 128)]
        public string TableCatalog { get; set; }

        [Field("TABLE_SCHEMA", FieldType.NVARCHAR, 128)]
        public string TableSchema { get; set; }

        [Field("TABLE_NAME", FieldType.NVARCHAR, 128)]
        public string TableName { get; set; }

        [Field("COLUMN_NAME", FieldType.NVARCHAR, 4000)]
        public string ColumnName { get; set; }

        [Field("IS_NULLABLE", FieldType.VARCHAR, 3)]
        public string IsNullableText { get; set; }

        [Field("DATA_TYPE", FieldType.NVARCHAR, 128)]
        public string DataType { get; set; }

    }
}
