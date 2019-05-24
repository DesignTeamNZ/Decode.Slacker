
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Person  {

    [Table("Person", "PersoPerso")]
    public class Person : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("EmailPromotion", FieldType.INT)]
		public Int32 EmailPromotion { get; set; }
					
		[Field("BusinessEntityID", FieldType.INT)]
		public Int32 BusinessEntityId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("Title", FieldType.NVARCHAR)]
		public String Title { get; set; }
					
		[Field("Suffix", FieldType.NVARCHAR)]
		public String Suffix { get; set; }
					
		[Field("PersonType", FieldType.NCHAR)]
		public String PersonType { get; set; }
					
		[Field("AdditionalContactInfo", FieldType.XML)]
		public String AdditionalContactInfo { get; set; }
					
		[Field("Demographics", FieldType.XML)]
		public String Demographics { get; set; }
					
		[Field("NameStyle", FieldType.BIT)]
		public Boolean NameStyle { get; set; }
					
		[Field("FirstName", FieldType.NVARCHAR)]
		public String FirstName { get; set; }
					
		[Field("MiddleName", FieldType.NVARCHAR)]
		public String MiddleName { get; set; }
					
		[Field("LastName", FieldType.NVARCHAR)]
		public String LastName { get; set; }
	
	#endregion Instance Properties

    }
}
