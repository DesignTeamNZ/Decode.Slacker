
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Sales  {

    [Table("CreditCard", "SalesCredi")]
    public class CreditCard : DataModel {
	#region Instance Properties
					
		[Field("ExpMonth", FieldType.TINY_INT)]
		public Int32 ExpMonth { get; set; }
					
		[Field("ExpYear", FieldType.SMALL_INT)]
		public Int16 ExpYear { get; set; }
					
		[Field("CreditCardID", FieldType.INT)]
		public Int32 CreditCardId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("CardType", FieldType.NVARCHAR)]
		public String CardType { get; set; }
					
		[Field("CardNumber", FieldType.NVARCHAR)]
		public String CardNumber { get; set; }
	
	#endregion Instance Properties

    }
}
