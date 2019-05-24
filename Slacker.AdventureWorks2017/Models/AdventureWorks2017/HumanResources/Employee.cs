
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.HumanResources  {

    [Table("Employee", "HumanEmplo")]
    public class Employee : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("BirthDate", FieldType.DATE)]
		public DateTime BirthDate { get; set; }
					
		[Field("HireDate", FieldType.DATE)]
		public DateTime HireDate { get; set; }
					
		[Field("VacationHours", FieldType.SMALL_INT)]
		public Int16 VacationHours { get; set; }
					
		[Field("SickLeaveHours", FieldType.SMALL_INT)]
		public Int16 SickLeaveHours { get; set; }
					
		[Field("OrganizationLevel", FieldType.SMALL_INT)]
		public Int16? OrganizationLevel { get; set; }
					
		[Field("BusinessEntityID", FieldType.INT)]
		public Int32 BusinessEntityId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("OrganizationNode", FieldType.HIERARCHY_ID)]
		public String OrganizationNode { get; set; }
					
		[Field("NationalIDNumber", FieldType.NVARCHAR)]
		public String NationalIdnUmber { get; set; }
					
		[Field("LoginID", FieldType.NVARCHAR)]
		public String LoginId { get; set; }
					
		[Field("JobTitle", FieldType.NVARCHAR)]
		public String JobTitle { get; set; }
					
		[Field("MaritalStatus", FieldType.NCHAR)]
		public String MaritalStatus { get; set; }
					
		[Field("Gender", FieldType.NCHAR)]
		public String Gender { get; set; }
					
		[Field("SalariedFlag", FieldType.BIT)]
		public Boolean SalariedFlag { get; set; }
					
		[Field("CurrentFlag", FieldType.BIT)]
		public Boolean CurrentFlag { get; set; }
	
	#endregion Instance Properties

    }
}
