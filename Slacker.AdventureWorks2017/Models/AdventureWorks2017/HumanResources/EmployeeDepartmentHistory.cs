
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.HumanResources  {

    [Table("EmployeeDepartmentHistory", "HumanEmplo")]
    public class EmployeeDepartmentHistory : DataModel {
	#region Instance Properties
					
		[Field("StartDate", FieldType.DATE)]
		public DateTime StartDate { get; set; }
					
		[Field("EndDate", FieldType.DATE)]
		public DateTime? EndDate { get; set; }
					
		[Field("ShiftID", FieldType.TINY_INT)]
		public Int32 ShiftId { get; set; }
					
		[Field("DepartmentID", FieldType.SMALL_INT)]
		public Int16 DepartmentId { get; set; }
					
		[Field("BusinessEntityID", FieldType.INT)]
		public Int32 BusinessEntityId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
	
	#endregion Instance Properties

    }
}
