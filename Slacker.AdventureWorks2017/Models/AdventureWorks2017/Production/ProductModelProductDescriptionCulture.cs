
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Production  {

    [Table("ProductModelProductDescriptionCulture", "ProduProdu")]
    public class ProductModelProductDescriptionCulture : DataModel {
	#region Instance Properties
					
		[Field("ProductModelID", FieldType.INT)]
		public Int32 ProductModelId { get; set; }
					
		[Field("ProductDescriptionID", FieldType.INT)]
		public Int32 ProductDescriptionId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("CultureID", FieldType.NCHAR)]
		public String CultureId { get; set; }
	
	#endregion Instance Properties

    }
}
