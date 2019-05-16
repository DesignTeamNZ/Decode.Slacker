
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Production  {

    [Table("WorkOrderRouting", "ProduWorkO")]
    public class WorkOrderRouting : DataModel {
	#region Instance Properties
					
		[Field("OperationSequence", FieldType.SMALL_INT)]
		public Int16 OperationSequence { get; set; }
					
		[Field("LocationID", FieldType.SMALL_INT)]
		public Int16 LocationId { get; set; }
					
		[Field("WorkOrderID", FieldType.INT)]
		public Int32 WorkOrderId { get; set; }
					
		[Field("ProductID", FieldType.INT)]
		public Int32 ProductId { get; set; }
					
		[Field("PlannedCost", FieldType.MONEY)]
		public Decimal PlannedCost { get; set; }
					
		[Field("ActualCost", FieldType.MONEY)]
		public Decimal? ActualCost { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("ScheduledStartDate", FieldType.DATETIME)]
		public DateTime ScheduledStartDate { get; set; }
					
		[Field("ScheduledEndDate", FieldType.DATETIME)]
		public DateTime ScheduledEndDate { get; set; }
					
		[Field("ActualStartDate", FieldType.DATETIME)]
		public DateTime? ActualStartDate { get; set; }
					
		[Field("ActualEndDate", FieldType.DATETIME)]
		public DateTime? ActualEndDate { get; set; }
					
		[Field("ActualResourceHrs", FieldType.DECIMAL)]
		public Decimal? ActualResourceHrs { get; set; }
	
	#endregion Instance Properties

    }
}
