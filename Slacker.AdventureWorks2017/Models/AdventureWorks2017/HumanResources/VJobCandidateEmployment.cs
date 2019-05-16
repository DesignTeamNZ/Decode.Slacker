
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.HumanResources  {

    [Table("vJobCandidateEmployment", "HumanVJobC")]
    public class VJobCandidateEmployment : DataModel {
	#region Instance Properties
					
		[Field("JobCandidateID", FieldType.INT)]
		public Int32 JobCandidateId { get; set; }
					
		[Field("Emp.StartDate", FieldType.DATETIME)]
		public DateTime? EmpStartDate { get; set; }
					
		[Field("Emp.EndDate", FieldType.DATETIME)]
		public DateTime? EmpEndDate { get; set; }
					
		[Field("Emp.OrgName", FieldType.NVARCHAR)]
		public String EmpOrgName { get; set; }
					
		[Field("Emp.JobTitle", FieldType.NVARCHAR)]
		public String EmpJobTitle { get; set; }
					
		[Field("Emp.Responsibility", FieldType.NVARCHAR)]
		public String EmpResponsibility { get; set; }
					
		[Field("Emp.FunctionCategory", FieldType.NVARCHAR)]
		public String EmpFunctionCategory { get; set; }
					
		[Field("Emp.IndustryCategory", FieldType.NVARCHAR)]
		public String EmpIndustryCategory { get; set; }
					
		[Field("Emp.Loc.CountryRegion", FieldType.NVARCHAR)]
		public String EmpLocCountryRegion { get; set; }
					
		[Field("Emp.Loc.State", FieldType.NVARCHAR)]
		public String EmpLocState { get; set; }
					
		[Field("Emp.Loc.City", FieldType.NVARCHAR)]
		public String EmpLocCity { get; set; }
	
	#endregion Instance Properties

    }
}
