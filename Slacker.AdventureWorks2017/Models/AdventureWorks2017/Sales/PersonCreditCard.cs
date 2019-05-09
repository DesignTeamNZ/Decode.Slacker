
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Sales  {

    [Table("PersonCreditCard", "SalesPerso")]
    public class PersonCreditCard : DataModel {
	#region Instance Properties
					
		[Field("BusinessEntityID", FieldType.INT)]
		public Int32 BusinessEntityId { get; set; }
					
		[Field("CreditCardID", FieldType.INT)]
		public Int32 CreditCardId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
	
	#endregion Instance Properties

    }
}
