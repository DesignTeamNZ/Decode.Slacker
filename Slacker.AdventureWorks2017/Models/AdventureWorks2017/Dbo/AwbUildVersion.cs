
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Dbo  {

    [Table("AWBuildVersion", "DboAwbUi")]
    public class AwbUildVersion : DataModel {
	#region Instance Properties
					
		[Field("SystemInformationID", FieldType.TINY_INT)]
		public Int32 SystemInformationId { get; set; }
					
		[Field("VersionDate", FieldType.DATETIME)]
		public DateTime VersionDate { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("Database Version", FieldType.NVARCHAR)]
		public String DatabaseVersion { get; set; }
	
	#endregion Instance Properties

    }
}
