
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Person  {

    [Table("Password", "PersoPassw")]
    public class Password : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("BusinessEntityID", FieldType.INT)]
		public Int32 BusinessEntityId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("PasswordHash", FieldType.VARCHAR)]
		public String PasswordHash { get; set; }
					
		[Field("PasswordSalt", FieldType.VARCHAR)]
		public String PasswordSalt { get; set; }
	
	#endregion Instance Properties

    }
}
