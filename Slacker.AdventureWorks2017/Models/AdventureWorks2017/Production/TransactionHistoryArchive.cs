
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Production  {

    [Table("TransactionHistoryArchive", "ProduTrans")]
    public class TransactionHistoryArchive : DataModel {
	#region Instance Properties
					
		[Field("TransactionID", FieldType.INT)]
		public Int32 TransactionId { get; set; }
					
		[Field("ProductID", FieldType.INT)]
		public Int32 ProductId { get; set; }
					
		[Field("ReferenceOrderID", FieldType.INT)]
		public Int32 ReferenceOrderId { get; set; }
					
		[Field("ReferenceOrderLineID", FieldType.INT)]
		public Int32 ReferenceOrderLineId { get; set; }
					
		[Field("Quantity", FieldType.INT)]
		public Int32 Quantity { get; set; }
					
		[Field("ActualCost", FieldType.MONEY)]
		public Decimal ActualCost { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("TransactionDate", FieldType.DATETIME)]
		public DateTime TransactionDate { get; set; }
					
		[Field("TransactionType", FieldType.NCHAR)]
		public String TransactionType { get; set; }
	
	#endregion Instance Properties

    }
}
