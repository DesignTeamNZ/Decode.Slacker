
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Person  {

    [Table("PersonPhone", "PersoPerso")]
    public class PersonPhone : DataModel {
	#region Instance Properties
					
		[Field("BusinessEntityID", FieldType.INT)]
		public Int32 BusinessEntityId { get; set; }
					
		[Field("PhoneNumberTypeID", FieldType.INT)]
		public Int32 PhoneNumberTypeId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("PhoneNumber", FieldType.NVARCHAR)]
		public String PhoneNumber { get; set; }
	
	#endregion Instance Properties

    }
}
