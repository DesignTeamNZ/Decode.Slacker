
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Person  {

    [Table("vAdditionalContactInfo", "PersoVAddi")]
    public class VAdditionalContactInfo : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("BusinessEntityID", FieldType.INT)]
		public Int32 BusinessEntityId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("TelephoneNumber", FieldType.NVARCHAR)]
		public String TelephoneNumber { get; set; }
					
		[Field("TelephoneSpecialInstructions", FieldType.NVARCHAR)]
		public String TelephoneSpecialInstructions { get; set; }
					
		[Field("Street", FieldType.NVARCHAR)]
		public String Street { get; set; }
					
		[Field("City", FieldType.NVARCHAR)]
		public String City { get; set; }
					
		[Field("StateProvince", FieldType.NVARCHAR)]
		public String StateProvince { get; set; }
					
		[Field("PostalCode", FieldType.NVARCHAR)]
		public String PostalCode { get; set; }
					
		[Field("CountryRegion", FieldType.NVARCHAR)]
		public String CountryRegion { get; set; }
					
		[Field("HomeAddressSpecialInstructions", FieldType.NVARCHAR)]
		public String HomeAddressSpecialInstructions { get; set; }
					
		[Field("EMailAddress", FieldType.NVARCHAR)]
		public String EmAilAddress { get; set; }
					
		[Field("EMailSpecialInstructions", FieldType.NVARCHAR)]
		public String EmAilSpecialInstructions { get; set; }
					
		[Field("EMailTelephoneNumber", FieldType.NVARCHAR)]
		public String EmAilTelephoneNumber { get; set; }
					
		[Field("FirstName", FieldType.NVARCHAR)]
		public String FirstName { get; set; }
					
		[Field("MiddleName", FieldType.NVARCHAR)]
		public String MiddleName { get; set; }
					
		[Field("LastName", FieldType.NVARCHAR)]
		public String LastName { get; set; }
	
	#endregion Instance Properties

    }
}
