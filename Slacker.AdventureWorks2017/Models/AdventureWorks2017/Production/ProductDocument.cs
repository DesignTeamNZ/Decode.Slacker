
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Production  {

    [Table("ProductDocument", "ProduProdu")]
    public class ProductDocument : DataModel {
	#region Instance Properties
					
		[Field("ProductID", FieldType.INT)]
		public Int32 ProductId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("DocumentNode", FieldType.HIERARCHY_ID)]
		public String DocumentNode { get; set; }
	
	#endregion Instance Properties

    }
}
