
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Sales  {

    [Table("ShoppingCartItem", "SalesShopp")]
    public class ShoppingCartItem : DataModel {
	#region Instance Properties
					
		[Field("Quantity", FieldType.INT)]
		public Int32 Quantity { get; set; }
					
		[Field("ProductID", FieldType.INT)]
		public Int32 ProductId { get; set; }
					
		[Field("ShoppingCartItemID", FieldType.INT)]
		public Int32 ShoppingCartItemId { get; set; }
					
		[Field("DateCreated", FieldType.DATETIME)]
		public DateTime DateCreated { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("ShoppingCartID", FieldType.NVARCHAR)]
		public String ShoppingCartId { get; set; }
	
	#endregion Instance Properties

    }
}
