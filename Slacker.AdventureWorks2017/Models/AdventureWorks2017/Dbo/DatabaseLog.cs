
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Dbo  {

    [Table("DatabaseLog", "DboDatab")]
    public class DatabaseLog : DataModel {
	#region Instance Properties
					
		[Field("DatabaseLogID", FieldType.INT)]
		public Int32 DatabaseLogId { get; set; }
					
		[Field("PostTime", FieldType.DATETIME)]
		public DateTime PostTime { get; set; }
					
		[Field("TSQL", FieldType.NVARCHAR)]
		public String Tsql { get; set; }
					
		[Field("XmlEvent", FieldType.XML)]
		public String XmlEvent { get; set; }
					
		[Field("DatabaseUser", FieldType.NVARCHAR)]
		public String DatabaseUser { get; set; }
					
		[Field("Event", FieldType.NVARCHAR)]
		public String Event { get; set; }
					
		[Field("Schema", FieldType.NVARCHAR)]
		public String Schema { get; set; }
					
		[Field("Object", FieldType.NVARCHAR)]
		public String Object { get; set; }
	
	#endregion Instance Properties

    }
}
