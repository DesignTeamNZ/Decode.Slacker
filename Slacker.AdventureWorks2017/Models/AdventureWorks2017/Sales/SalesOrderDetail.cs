
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Sales  {

    [Table("SalesOrderDetail", "SalesSales")]
    public class SalesOrderDetail : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("OrderQty", FieldType.SMALL_INT)]
		public Int16 OrderQty { get; set; }
					
		[Field("ProductID", FieldType.INT)]
		public Int32 ProductId { get; set; }
					
		[Field("SpecialOfferID", FieldType.INT)]
		public Int32 SpecialOfferId { get; set; }
					
		[Field("SalesOrderID", FieldType.INT)]
		public Int32 SalesOrderId { get; set; }
					
		[Field("SalesOrderDetailID", FieldType.INT)]
		public Int32 SalesOrderDetailId { get; set; }
					
		[Field("UnitPrice", FieldType.MONEY)]
		public Decimal UnitPrice { get; set; }
					
		[Field("UnitPriceDiscount", FieldType.MONEY)]
		public Decimal UnitPriceDiscount { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("LineTotal", FieldType.NUMERIC)]
		public Decimal LineTotal { get; set; }
					
		[Field("CarrierTrackingNumber", FieldType.NVARCHAR)]
		public String CarrierTrackingNumber { get; set; }
	
	#endregion Instance Properties

    }
}
