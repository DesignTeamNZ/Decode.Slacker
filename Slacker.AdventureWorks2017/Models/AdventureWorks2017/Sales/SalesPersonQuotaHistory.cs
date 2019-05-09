
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Sales  {

    [Table("SalesPersonQuotaHistory", "SalesSales")]
    public class SalesPersonQuotaHistory : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("BusinessEntityID", FieldType.INT)]
		public Int32 BusinessEntityId { get; set; }
					
		[Field("SalesQuota", FieldType.MONEY)]
		public Decimal SalesQuota { get; set; }
					
		[Field("QuotaDate", FieldType.DATETIME)]
		public DateTime QuotaDate { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
	
	#endregion Instance Properties

    }
}
