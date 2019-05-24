
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Sales  {

    [Table("CurrencyRate", "SalesCurre")]
    public class CurrencyRate : DataModel {
	#region Instance Properties
					
		[Field("CurrencyRateID", FieldType.INT)]
		public Int32 CurrencyRateId { get; set; }
					
		[Field("AverageRate", FieldType.MONEY)]
		public Decimal AverageRate { get; set; }
					
		[Field("EndOfDayRate", FieldType.MONEY)]
		public Decimal EndOfDayRate { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("CurrencyRateDate", FieldType.DATETIME)]
		public DateTime CurrencyRateDate { get; set; }
					
		[Field("FromCurrencyCode", FieldType.NCHAR)]
		public String FromCurrencyCode { get; set; }
					
		[Field("ToCurrencyCode", FieldType.NCHAR)]
		public String ToCurrencyCode { get; set; }
	
	#endregion Instance Properties

    }
}
