
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Production  {

    [Table("ProductProductPhoto", "ProduProdu")]
    public class ProductProductPhoto : DataModel {
	#region Instance Properties
					
		[Field("ProductID", FieldType.INT)]
		public Int32 ProductId { get; set; }
					
		[Field("ProductPhotoID", FieldType.INT)]
		public Int32 ProductPhotoId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("Primary", FieldType.BIT)]
		public Boolean Primary { get; set; }
	
	#endregion Instance Properties

    }
}
