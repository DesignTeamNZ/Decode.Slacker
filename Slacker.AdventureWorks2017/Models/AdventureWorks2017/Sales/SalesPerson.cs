
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Sales  {

    [Table("SalesPerson", "SalesSales")]
    public class SalesPerson : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("BusinessEntityID", FieldType.INT)]
		public Int32 BusinessEntityId { get; set; }
					
		[Field("TerritoryID", FieldType.INT)]
		public Int32? TerritoryId { get; set; }
					
		[Field("SalesYTD", FieldType.MONEY)]
		public Decimal SalesYtd { get; set; }
					
		[Field("SalesLastYear", FieldType.MONEY)]
		public Decimal SalesLastYear { get; set; }
					
		[Field("SalesQuota", FieldType.MONEY)]
		public Decimal? SalesQuota { get; set; }
					
		[Field("Bonus", FieldType.MONEY)]
		public Decimal Bonus { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("CommissionPct", FieldType.SMALL_MONEY)]
		public Decimal CommissionPct { get; set; }
	
	#endregion Instance Properties

    }
}
