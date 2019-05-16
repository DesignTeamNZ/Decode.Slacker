
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Purchasing  {

    [Table("PurchaseOrderDetail", "PurchPurch")]
    public class PurchaseOrderDetail : DataModel {
	#region Instance Properties
					
		[Field("OrderQty", FieldType.SMALL_INT)]
		public Int16 OrderQty { get; set; }
					
		[Field("PurchaseOrderID", FieldType.INT)]
		public Int32 PurchaseOrderId { get; set; }
					
		[Field("PurchaseOrderDetailID", FieldType.INT)]
		public Int32 PurchaseOrderDetailId { get; set; }
					
		[Field("ProductID", FieldType.INT)]
		public Int32 ProductId { get; set; }
					
		[Field("UnitPrice", FieldType.MONEY)]
		public Decimal UnitPrice { get; set; }
					
		[Field("LineTotal", FieldType.MONEY)]
		public Decimal LineTotal { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("DueDate", FieldType.DATETIME)]
		public DateTime DueDate { get; set; }
					
		[Field("ReceivedQty", FieldType.DECIMAL)]
		public Decimal ReceivedQty { get; set; }
					
		[Field("RejectedQty", FieldType.DECIMAL)]
		public Decimal RejectedQty { get; set; }
					
		[Field("StockedQty", FieldType.DECIMAL)]
		public Decimal StockedQty { get; set; }
	
	#endregion Instance Properties

    }
}
