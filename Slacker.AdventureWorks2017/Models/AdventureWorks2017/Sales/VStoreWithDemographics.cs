
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Sales  {

    [Table("vStoreWithDemographics", "SalesVStor")]
    public class VStoreWithDemographics : DataModel {
	#region Instance Properties
					
		[Field("BusinessEntityID", FieldType.INT)]
		public Int32 BusinessEntityId { get; set; }
					
		[Field("YearOpened", FieldType.INT)]
		public Int32? YearOpened { get; set; }
					
		[Field("SquareFeet", FieldType.INT)]
		public Int32? SquareFeet { get; set; }
					
		[Field("NumberEmployees", FieldType.INT)]
		public Int32? NumberEmployees { get; set; }
					
		[Field("AnnualSales", FieldType.MONEY)]
		public Decimal? AnnualSales { get; set; }
					
		[Field("AnnualRevenue", FieldType.MONEY)]
		public Decimal? AnnualRevenue { get; set; }
					
		[Field("BankName", FieldType.NVARCHAR)]
		public String BankName { get; set; }
					
		[Field("BusinessType", FieldType.NVARCHAR)]
		public String BusinessType { get; set; }
					
		[Field("Brands", FieldType.NVARCHAR)]
		public String Brands { get; set; }
					
		[Field("Internet", FieldType.NVARCHAR)]
		public String Internet { get; set; }
					
		[Field("Specialty", FieldType.NVARCHAR)]
		public String Specialty { get; set; }
					
		[Field("Name", FieldType.NVARCHAR)]
		public String Name { get; set; }
	
	#endregion Instance Properties

    }
}
