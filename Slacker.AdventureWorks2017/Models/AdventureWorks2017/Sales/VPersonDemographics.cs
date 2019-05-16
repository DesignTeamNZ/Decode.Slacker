
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Sales  {

    [Table("vPersonDemographics", "SalesVPers")]
    public class VPersonDemographics : DataModel {
	#region Instance Properties
					
		[Field("BusinessEntityID", FieldType.INT)]
		public Int32 BusinessEntityId { get; set; }
					
		[Field("NumberCarsOwned", FieldType.INT)]
		public Int32? NumberCarsOwned { get; set; }
					
		[Field("TotalChildren", FieldType.INT)]
		public Int32? TotalChildren { get; set; }
					
		[Field("NumberChildrenAtHome", FieldType.INT)]
		public Int32? NumberChildrenAtHome { get; set; }
					
		[Field("TotalPurchaseYTD", FieldType.MONEY)]
		public Decimal? TotalPurchaseYtd { get; set; }
					
		[Field("DateFirstPurchase", FieldType.DATETIME)]
		public DateTime? DateFirstPurchase { get; set; }
					
		[Field("BirthDate", FieldType.DATETIME)]
		public DateTime? BirthDate { get; set; }
					
		[Field("HomeOwnerFlag", FieldType.BIT)]
		public Boolean? HomeOwnerFlag { get; set; }
					
		[Field("MaritalStatus", FieldType.NVARCHAR)]
		public String MaritalStatus { get; set; }
					
		[Field("YearlyIncome", FieldType.NVARCHAR)]
		public String YearlyIncome { get; set; }
					
		[Field("Gender", FieldType.NVARCHAR)]
		public String Gender { get; set; }
					
		[Field("Education", FieldType.NVARCHAR)]
		public String Education { get; set; }
					
		[Field("Occupation", FieldType.NVARCHAR)]
		public String Occupation { get; set; }
	
	#endregion Instance Properties

    }
}
