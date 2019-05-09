
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Sales  {

    [Table("SalesTaxRate", "SalesSales")]
    public class SalesTaxRate : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("TaxType", FieldType.TINY_INT)]
		public Int32 TaxType { get; set; }
					
		[Field("SalesTaxRateID", FieldType.INT)]
		public Int32 SalesTaxRateId { get; set; }
					
		[Field("StateProvinceID", FieldType.INT)]
		public Int32 StateProvinceId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("TaxRate", FieldType.SMALL_MONEY)]
		public Decimal TaxRate { get; set; }
					
		[Field("Name", FieldType.NVARCHAR)]
		public String Name { get; set; }
	
	#endregion Instance Properties

    }
}
