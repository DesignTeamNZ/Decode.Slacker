
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Dbo  {

    [Table("ErrorLog", "DboError")]
    public class ErrorLog : DataModel {
	#region Instance Properties
					
		[Field("ErrorNumber", FieldType.INT)]
		public Int32 ErrorNumber { get; set; }
					
		[Field("ErrorSeverity", FieldType.INT)]
		public Int32? ErrorSeverity { get; set; }
					
		[Field("ErrorState", FieldType.INT)]
		public Int32? ErrorState { get; set; }
					
		[Field("ErrorLogID", FieldType.INT)]
		public Int32 ErrorLogId { get; set; }
					
		[Field("ErrorLine", FieldType.INT)]
		public Int32? ErrorLine { get; set; }
					
		[Field("ErrorTime", FieldType.DATETIME)]
		public DateTime ErrorTime { get; set; }
					
		[Field("ErrorMessage", FieldType.NVARCHAR)]
		public String ErrorMessage { get; set; }
					
		[Field("ErrorProcedure", FieldType.NVARCHAR)]
		public String ErrorProcedure { get; set; }
					
		[Field("UserName", FieldType.NVARCHAR)]
		public String UserName { get; set; }
	
	#endregion Instance Properties

    }
}
