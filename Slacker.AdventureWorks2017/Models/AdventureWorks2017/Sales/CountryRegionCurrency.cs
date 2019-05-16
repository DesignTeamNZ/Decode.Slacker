
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Sales  {

    [Table("CountryRegionCurrency", "SalesCount")]
    public class CountryRegionCurrency : DataModel {
	#region Instance Properties
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("CountryRegionCode", FieldType.NVARCHAR)]
		public String CountryRegionCode { get; set; }
					
		[Field("CurrencyCode", FieldType.NCHAR)]
		public String CurrencyCode { get; set; }
	
	#endregion Instance Properties

    }
}
