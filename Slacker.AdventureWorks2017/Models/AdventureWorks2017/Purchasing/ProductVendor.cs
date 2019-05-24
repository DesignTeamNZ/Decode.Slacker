
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Purchasing  {

    [Table("ProductVendor", "PurchProdu")]
    public class ProductVendor : DataModel {
	#region Instance Properties
					
		[Field("MinOrderQty", FieldType.INT)]
		public Int32 MinOrderQty { get; set; }
					
		[Field("MaxOrderQty", FieldType.INT)]
		public Int32 MaxOrderQty { get; set; }
					
		[Field("OnOrderQty", FieldType.INT)]
		public Int32? OnOrderQty { get; set; }
					
		[Field("ProductID", FieldType.INT)]
		public Int32 ProductId { get; set; }
					
		[Field("BusinessEntityID", FieldType.INT)]
		public Int32 BusinessEntityId { get; set; }
					
		[Field("AverageLeadTime", FieldType.INT)]
		public Int32 AverageLeadTime { get; set; }
					
		[Field("StandardPrice", FieldType.MONEY)]
		public Decimal StandardPrice { get; set; }
					
		[Field("LastReceiptCost", FieldType.MONEY)]
		public Decimal? LastReceiptCost { get; set; }
					
		[Field("LastReceiptDate", FieldType.DATETIME)]
		public DateTime? LastReceiptDate { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("UnitMeasureCode", FieldType.NCHAR)]
		public String UnitMeasureCode { get; set; }
	
	#endregion Instance Properties

    }
}
