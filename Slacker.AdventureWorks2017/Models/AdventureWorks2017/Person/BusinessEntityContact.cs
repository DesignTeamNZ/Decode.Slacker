
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Person  {

    [Table("BusinessEntityContact", "PersoBusin")]
    public class BusinessEntityContact : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("BusinessEntityID", FieldType.INT)]
		public Int32 BusinessEntityId { get; set; }
					
		[Field("PersonID", FieldType.INT)]
		public Int32 PersonId { get; set; }
					
		[Field("ContactTypeID", FieldType.INT)]
		public Int32 ContactTypeId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
	
	#endregion Instance Properties

    }
}
