
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Production  {

    [Table("ProductInventory", "ProduProdu")]
    public class ProductInventory : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("Bin", FieldType.TINY_INT)]
		public Int32 Bin { get; set; }
					
		[Field("Quantity", FieldType.SMALL_INT)]
		public Int16 Quantity { get; set; }
					
		[Field("LocationID", FieldType.SMALL_INT)]
		public Int16 LocationId { get; set; }
					
		[Field("ProductID", FieldType.INT)]
		public Int32 ProductId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("Shelf", FieldType.NVARCHAR)]
		public String Shelf { get; set; }
	
	#endregion Instance Properties

    }
}
