
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Purchasing  {

    [Table("Vendor", "PurchVendo")]
    public class Vendor : DataModel {
	#region Instance Properties
					
		[Field("CreditRating", FieldType.TINY_INT)]
		public Int32 CreditRating { get; set; }
					
		[Field("BusinessEntityID", FieldType.INT)]
		public Int32 BusinessEntityId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("PurchasingWebServiceURL", FieldType.NVARCHAR)]
		public String PurchasingWebServiceUrl { get; set; }
					
		[Field("AccountNumber", FieldType.NVARCHAR)]
		public String AccountNumber { get; set; }
					
		[Field("PreferredVendorStatus", FieldType.BIT)]
		public Boolean PreferredVendorStatus { get; set; }
					
		[Field("ActiveFlag", FieldType.BIT)]
		public Boolean ActiveFlag { get; set; }
					
		[Field("Name", FieldType.NVARCHAR)]
		public String Name { get; set; }
	
	#endregion Instance Properties

    }
}
