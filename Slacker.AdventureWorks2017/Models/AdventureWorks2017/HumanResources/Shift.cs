
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.HumanResources  {

    [Table("Shift", "HumanShift")]
    public class Shift : DataModel {
	#region Instance Properties
					
		[Field("StartTime", FieldType.TIME)]
		public DateTime StartTime { get; set; }
					
		[Field("EndTime", FieldType.TIME)]
		public DateTime EndTime { get; set; }
					
		[Field("ShiftID", FieldType.TINY_INT)]
		public Int32 ShiftId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("Name", FieldType.NVARCHAR)]
		public String Name { get; set; }
	
	#endregion Instance Properties

    }
}
