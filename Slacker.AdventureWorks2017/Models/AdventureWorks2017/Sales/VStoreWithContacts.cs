
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Sales  {

    [Table("vStoreWithContacts", "SalesVStor")]
    public class VStoreWithContacts : DataModel {
	#region Instance Properties
					
		[Field("EmailPromotion", FieldType.INT)]
		public Int32 EmailPromotion { get; set; }
					
		[Field("BusinessEntityID", FieldType.INT)]
		public Int32 BusinessEntityId { get; set; }
					
		[Field("Suffix", FieldType.NVARCHAR)]
		public String Suffix { get; set; }
					
		[Field("EmailAddress", FieldType.NVARCHAR)]
		public String EmailAddress { get; set; }
					
		[Field("Title", FieldType.NVARCHAR)]
		public String Title { get; set; }
					
		[Field("FirstName", FieldType.NVARCHAR)]
		public String FirstName { get; set; }
					
		[Field("MiddleName", FieldType.NVARCHAR)]
		public String MiddleName { get; set; }
					
		[Field("LastName", FieldType.NVARCHAR)]
		public String LastName { get; set; }
					
		[Field("Name", FieldType.NVARCHAR)]
		public String Name { get; set; }
					
		[Field("ContactType", FieldType.NVARCHAR)]
		public String ContactType { get; set; }
					
		[Field("PhoneNumberType", FieldType.NVARCHAR)]
		public String PhoneNumberType { get; set; }
					
		[Field("PhoneNumber", FieldType.NVARCHAR)]
		public String PhoneNumber { get; set; }
	
	#endregion Instance Properties

    }
}
