
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Sales  {

    [Table("vSalesPersonSalesByFiscalYears", "SalesVSale")]
    public class VSalesPersonSalesByFiscalYears : DataModel {
	#region Instance Properties
					
		[Field("SalesPersonID", FieldType.INT)]
		public Int32? SalesPersonId { get; set; }
					
		[Field("2002", FieldType.MONEY)]
		public Decimal? _2002 { get; set; }
					
		[Field("2003", FieldType.MONEY)]
		public Decimal? _2003 { get; set; }
					
		[Field("2004", FieldType.MONEY)]
		public Decimal? _2004 { get; set; }
					
		[Field("FullName", FieldType.NVARCHAR)]
		public String FullName { get; set; }
					
		[Field("JobTitle", FieldType.NVARCHAR)]
		public String JobTitle { get; set; }
					
		[Field("SalesTerritory", FieldType.NVARCHAR)]
		public String SalesTerritory { get; set; }
	
	#endregion Instance Properties

    }
}
