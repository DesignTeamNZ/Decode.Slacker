
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.HumanResources  {

    [Table("Department", "HumanDepar")]
    public class Department : DataModel {
	#region Instance Properties
					
		[Field("DepartmentID", FieldType.SMALL_INT)]
		public Int16 DepartmentId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("Name", FieldType.NVARCHAR)]
		public String Name { get; set; }
					
		[Field("GroupName", FieldType.NVARCHAR)]
		public String GroupName { get; set; }
	
	#endregion Instance Properties

    }
}
