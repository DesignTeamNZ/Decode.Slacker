
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.HumanResources  {

    [Table("vJobCandidateEducation", "HumanVJobC")]
    public class VJobCandidateEducation : DataModel {
	#region Instance Properties
					
		[Field("JobCandidateID", FieldType.INT)]
		public Int32 JobCandidateId { get; set; }
					
		[Field("Edu.StartDate", FieldType.DATETIME)]
		public DateTime? EduStartDate { get; set; }
					
		[Field("Edu.EndDate", FieldType.DATETIME)]
		public DateTime? EduEndDate { get; set; }
					
		[Field("Edu.Degree", FieldType.NVARCHAR)]
		public String EduDegree { get; set; }
					
		[Field("Edu.Major", FieldType.NVARCHAR)]
		public String EduMajor { get; set; }
					
		[Field("Edu.Minor", FieldType.NVARCHAR)]
		public String EduMinor { get; set; }
					
		[Field("Edu.GPA", FieldType.NVARCHAR)]
		public String EduGpa { get; set; }
					
		[Field("Edu.GPAScale", FieldType.NVARCHAR)]
		public String EduGpasCale { get; set; }
					
		[Field("Edu.School", FieldType.NVARCHAR)]
		public String EduSchool { get; set; }
					
		[Field("Edu.Loc.CountryRegion", FieldType.NVARCHAR)]
		public String EduLocCountryRegion { get; set; }
					
		[Field("Edu.Loc.State", FieldType.NVARCHAR)]
		public String EduLocState { get; set; }
					
		[Field("Edu.Loc.City", FieldType.NVARCHAR)]
		public String EduLocCity { get; set; }
					
		[Field("Edu.Level", FieldType.NVARCHAR)]
		public String EduLevel { get; set; }
	
	#endregion Instance Properties

    }
}
