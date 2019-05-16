
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Person  {

    [Table("PhoneNumberType", "PersoPhone")]
    public class PhoneNumberType : DataModel {
	#region Instance Properties
					
		[Field("PhoneNumberTypeID", FieldType.INT)]
		public Int32 PhoneNumberTypeId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("Name", FieldType.NVARCHAR)]
		public String Name { get; set; }
	
	#endregion Instance Properties

    }
}
