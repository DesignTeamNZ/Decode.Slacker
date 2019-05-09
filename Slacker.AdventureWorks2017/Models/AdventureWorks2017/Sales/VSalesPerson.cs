
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Sales  {

    [Table("vSalesPerson", "SalesVSale")]
    public class VSalesPerson : DataModel {
	#region Instance Properties
					
		[Field("BusinessEntityID", FieldType.INT)]
		public Int32 BusinessEntityId { get; set; }
					
		[Field("EmailPromotion", FieldType.INT)]
		public Int32 EmailPromotion { get; set; }
					
		[Field("SalesQuota", FieldType.MONEY)]
		public Decimal? SalesQuota { get; set; }
					
		[Field("SalesYTD", FieldType.MONEY)]
		public Decimal SalesYtd { get; set; }
					
		[Field("SalesLastYear", FieldType.MONEY)]
		public Decimal SalesLastYear { get; set; }
					
		[Field("Suffix", FieldType.NVARCHAR)]
		public String Suffix { get; set; }
					
		[Field("JobTitle", FieldType.NVARCHAR)]
		public String JobTitle { get; set; }
					
		[Field("Title", FieldType.NVARCHAR)]
		public String Title { get; set; }
					
		[Field("EmailAddress", FieldType.NVARCHAR)]
		public String EmailAddress { get; set; }
					
		[Field("TerritoryGroup", FieldType.NVARCHAR)]
		public String TerritoryGroup { get; set; }
					
		[Field("AddressLine1", FieldType.NVARCHAR)]
		public String AddressLine1 { get; set; }
					
		[Field("AddressLine2", FieldType.NVARCHAR)]
		public String AddressLine2 { get; set; }
					
		[Field("City", FieldType.NVARCHAR)]
		public String City { get; set; }
					
		[Field("PostalCode", FieldType.NVARCHAR)]
		public String PostalCode { get; set; }
					
		[Field("CountryRegionName", FieldType.NVARCHAR)]
		public String CountryRegionName { get; set; }
					
		[Field("TerritoryName", FieldType.NVARCHAR)]
		public String TerritoryName { get; set; }
					
		[Field("PhoneNumberType", FieldType.NVARCHAR)]
		public String PhoneNumberType { get; set; }
					
		[Field("StateProvinceName", FieldType.NVARCHAR)]
		public String StateProvinceName { get; set; }
					
		[Field("FirstName", FieldType.NVARCHAR)]
		public String FirstName { get; set; }
					
		[Field("MiddleName", FieldType.NVARCHAR)]
		public String MiddleName { get; set; }
					
		[Field("LastName", FieldType.NVARCHAR)]
		public String LastName { get; set; }
					
		[Field("PhoneNumber", FieldType.NVARCHAR)]
		public String PhoneNumber { get; set; }
	
	#endregion Instance Properties

    }
}
