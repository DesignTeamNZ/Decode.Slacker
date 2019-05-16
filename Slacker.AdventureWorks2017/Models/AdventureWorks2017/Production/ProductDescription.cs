
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Production  {

    [Table("ProductDescription", "ProduProdu")]
    public class ProductDescription : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("ProductDescriptionID", FieldType.INT)]
		public Int32 ProductDescriptionId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("Description", FieldType.NVARCHAR)]
		public String Description { get; set; }
	
	#endregion Instance Properties

    }
}
