
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Production  {

    [Table("Document", "ProduDocum")]
    public class Document : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("Status", FieldType.TINY_INT)]
		public Int32 Status { get; set; }
					
		[Field("DocumentLevel", FieldType.SMALL_INT)]
		public Int16? DocumentLevel { get; set; }
					
		[Field("ChangeNumber", FieldType.INT)]
		public Int32 ChangeNumber { get; set; }
					
		[Field("Owner", FieldType.INT)]
		public Int32 Owner { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("FolderFlag", FieldType.BIT)]
		public Boolean FolderFlag { get; set; }
					
		[Field("DocumentNode", FieldType.HIERARCHY_ID)]
		public String DocumentNode { get; set; }
					
		[Field("Document", FieldType.NOT_SPECIFIED)]
		public String _Document { get; set; }
					
		[Field("FileName", FieldType.NVARCHAR)]
		public String FileName { get; set; }
					
		[Field("FileExtension", FieldType.NVARCHAR)]
		public String FileExtension { get; set; }
					
		[Field("Title", FieldType.NVARCHAR)]
		public String Title { get; set; }
					
		[Field("DocumentSummary", FieldType.NVARCHAR)]
		public String DocumentSummary { get; set; }
					
		[Field("Revision", FieldType.NCHAR)]
		public String Revision { get; set; }
	
	#endregion Instance Properties

    }
}
