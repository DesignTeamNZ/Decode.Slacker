
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Production  {

    [Table("ProductModelIllustration", "ProduProdu")]
    public class ProductModelIllustration : DataModel {
	#region Instance Properties
					
		[Field("ProductModelID", FieldType.INT)]
		public Int32 ProductModelId { get; set; }
					
		[Field("IllustrationID", FieldType.INT)]
		public Int32 IllustrationId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
	
	#endregion Instance Properties

    }
}
