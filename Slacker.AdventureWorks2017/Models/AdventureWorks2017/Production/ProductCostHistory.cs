
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Production  {

    [Table("ProductCostHistory", "ProduProdu")]
    public class ProductCostHistory : DataModel {
	#region Instance Properties
					
		[Field("ProductID", FieldType.INT)]
		public Int32 ProductId { get; set; }
					
		[Field("StandardCost", FieldType.MONEY)]
		public Decimal StandardCost { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("StartDate", FieldType.DATETIME)]
		public DateTime StartDate { get; set; }
					
		[Field("EndDate", FieldType.DATETIME)]
		public DateTime? EndDate { get; set; }
	
	#endregion Instance Properties

    }
}
