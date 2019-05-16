
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Person  {

    [Table("EmailAddress", "PersoEmail")]
    public class EmailAddress : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("BusinessEntityID", FieldType.INT)]
		public Int32 BusinessEntityId { get; set; }
					
		[Field("EmailAddressID", FieldType.INT)]
		public Int32 EmailAddressId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("EmailAddress", FieldType.NVARCHAR)]
		public String _EmailAddress { get; set; }
	
	#endregion Instance Properties

    }
}
