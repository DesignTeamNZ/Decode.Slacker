
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.HumanResources  {

    [Table("vEmployeeDepartment", "HumanVEmpl")]
    public class VEmployeeDepartment : DataModel {
	#region Instance Properties
					
		[Field("StartDate", FieldType.DATE)]
		public DateTime StartDate { get; set; }
					
		[Field("BusinessEntityID", FieldType.INT)]
		public Int32 BusinessEntityId { get; set; }
					
		[Field("Title", FieldType.NVARCHAR)]
		public String Title { get; set; }
					
		[Field("Suffix", FieldType.NVARCHAR)]
		public String Suffix { get; set; }
					
		[Field("JobTitle", FieldType.NVARCHAR)]
		public String JobTitle { get; set; }
					
		[Field("Department", FieldType.NVARCHAR)]
		public String Department { get; set; }
					
		[Field("GroupName", FieldType.NVARCHAR)]
		public String GroupName { get; set; }
					
		[Field("FirstName", FieldType.NVARCHAR)]
		public String FirstName { get; set; }
					
		[Field("MiddleName", FieldType.NVARCHAR)]
		public String MiddleName { get; set; }
					
		[Field("LastName", FieldType.NVARCHAR)]
		public String LastName { get; set; }
	
	#endregion Instance Properties

    }
}
