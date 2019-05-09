
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Purchasing  {

    [Table("PurchaseOrderHeader", "PurchPurch")]
    public class PurchaseOrderHeader : DataModel {
	#region Instance Properties
					
		[Field("RevisionNumber", FieldType.TINY_INT)]
		public Int32 RevisionNumber { get; set; }
					
		[Field("Status", FieldType.TINY_INT)]
		public Int32 Status { get; set; }
					
		[Field("EmployeeID", FieldType.INT)]
		public Int32 EmployeeId { get; set; }
					
		[Field("VendorID", FieldType.INT)]
		public Int32 VendorId { get; set; }
					
		[Field("ShipMethodID", FieldType.INT)]
		public Int32 ShipMethodId { get; set; }
					
		[Field("PurchaseOrderID", FieldType.INT)]
		public Int32 PurchaseOrderId { get; set; }
					
		[Field("SubTotal", FieldType.MONEY)]
		public Decimal SubTotal { get; set; }
					
		[Field("TaxAmt", FieldType.MONEY)]
		public Decimal TaxAmt { get; set; }
					
		[Field("Freight", FieldType.MONEY)]
		public Decimal Freight { get; set; }
					
		[Field("TotalDue", FieldType.MONEY)]
		public Decimal TotalDue { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("OrderDate", FieldType.DATETIME)]
		public DateTime OrderDate { get; set; }
					
		[Field("ShipDate", FieldType.DATETIME)]
		public DateTime? ShipDate { get; set; }
	
	#endregion Instance Properties

    }
}
