
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Sales  {

    [Table("SalesTerritory", "SalesSales")]
    public class SalesTerritory : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("TerritoryID", FieldType.INT)]
		public Int32 TerritoryId { get; set; }
					
		[Field("SalesYTD", FieldType.MONEY)]
		public Decimal SalesYtd { get; set; }
					
		[Field("SalesLastYear", FieldType.MONEY)]
		public Decimal SalesLastYear { get; set; }
					
		[Field("CostYTD", FieldType.MONEY)]
		public Decimal CostYtd { get; set; }
					
		[Field("CostLastYear", FieldType.MONEY)]
		public Decimal CostLastYear { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("CountryRegionCode", FieldType.NVARCHAR)]
		public String CountryRegionCode { get; set; }
					
		[Field("Group", FieldType.NVARCHAR)]
		public String Group { get; set; }
					
		[Field("Name", FieldType.NVARCHAR)]
		public String Name { get; set; }
	
	#endregion Instance Properties

    }
}
