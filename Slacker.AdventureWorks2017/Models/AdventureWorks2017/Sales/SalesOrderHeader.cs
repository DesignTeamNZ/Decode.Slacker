
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Sales  {

    [Table("SalesOrderHeader", "SalesSales")]
    public class SalesOrderHeader : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("RevisionNumber", FieldType.TINY_INT)]
		public Int32 RevisionNumber { get; set; }
					
		[Field("Status", FieldType.TINY_INT)]
		public Int32 Status { get; set; }
					
		[Field("SalesOrderID", FieldType.INT)]
		public Int32 SalesOrderId { get; set; }
					
		[Field("CurrencyRateID", FieldType.INT)]
		public Int32? CurrencyRateId { get; set; }
					
		[Field("CustomerID", FieldType.INT)]
		public Int32 CustomerId { get; set; }
					
		[Field("SalesPersonID", FieldType.INT)]
		public Int32? SalesPersonId { get; set; }
					
		[Field("TerritoryID", FieldType.INT)]
		public Int32? TerritoryId { get; set; }
					
		[Field("BillToAddressID", FieldType.INT)]
		public Int32 BillToAddressId { get; set; }
					
		[Field("ShipToAddressID", FieldType.INT)]
		public Int32 ShipToAddressId { get; set; }
					
		[Field("ShipMethodID", FieldType.INT)]
		public Int32 ShipMethodId { get; set; }
					
		[Field("CreditCardID", FieldType.INT)]
		public Int32? CreditCardId { get; set; }
					
		[Field("SubTotal", FieldType.MONEY)]
		public Decimal SubTotal { get; set; }
					
		[Field("TaxAmt", FieldType.MONEY)]
		public Decimal TaxAmt { get; set; }
					
		[Field("Freight", FieldType.MONEY)]
		public Decimal Freight { get; set; }
					
		[Field("TotalDue", FieldType.MONEY)]
		public Decimal TotalDue { get; set; }
					
		[Field("OrderDate", FieldType.DATETIME)]
		public DateTime OrderDate { get; set; }
					
		[Field("DueDate", FieldType.DATETIME)]
		public DateTime DueDate { get; set; }
					
		[Field("ShipDate", FieldType.DATETIME)]
		public DateTime? ShipDate { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("CreditCardApprovalCode", FieldType.VARCHAR)]
		public String CreditCardApprovalCode { get; set; }
					
		[Field("Comment", FieldType.NVARCHAR)]
		public String Comment { get; set; }
					
		[Field("SalesOrderNumber", FieldType.NVARCHAR)]
		public String SalesOrderNumber { get; set; }
					
		[Field("AccountNumber", FieldType.NVARCHAR)]
		public String AccountNumber { get; set; }
					
		[Field("OnlineOrderFlag", FieldType.BIT)]
		public Boolean OnlineOrderFlag { get; set; }
					
		[Field("PurchaseOrderNumber", FieldType.NVARCHAR)]
		public String PurchaseOrderNumber { get; set; }
	
	#endregion Instance Properties

    }
}
