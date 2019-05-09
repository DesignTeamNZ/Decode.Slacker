
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Production  {

    [Table("WorkOrder", "ProduWorkO")]
    public class WorkOrder : DataModel {
	#region Instance Properties
					
		[Field("ScrappedQty", FieldType.SMALL_INT)]
		public Int16 ScrappedQty { get; set; }
					
		[Field("ScrapReasonID", FieldType.SMALL_INT)]
		public Int16? ScrapReasonId { get; set; }
					
		[Field("WorkOrderID", FieldType.INT)]
		public Int32 WorkOrderId { get; set; }
					
		[Field("ProductID", FieldType.INT)]
		public Int32 ProductId { get; set; }
					
		[Field("OrderQty", FieldType.INT)]
		public Int32 OrderQty { get; set; }
					
		[Field("StockedQty", FieldType.INT)]
		public Int32 StockedQty { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("StartDate", FieldType.DATETIME)]
		public DateTime StartDate { get; set; }
					
		[Field("EndDate", FieldType.DATETIME)]
		public DateTime? EndDate { get; set; }
					
		[Field("DueDate", FieldType.DATETIME)]
		public DateTime DueDate { get; set; }
	
	#endregion Instance Properties

    }
}
