
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Production  {

    [Table("ProductPhoto", "ProduProdu")]
    public class ProductPhoto : DataModel {
	#region Instance Properties
					
		[Field("ProductPhotoID", FieldType.INT)]
		public Int32 ProductPhotoId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("ThumbNailPhoto", FieldType.NOT_SPECIFIED)]
		public String ThumbNailPhoto { get; set; }
					
		[Field("LargePhoto", FieldType.NOT_SPECIFIED)]
		public String LargePhoto { get; set; }
					
		[Field("LargePhotoFileName", FieldType.NVARCHAR)]
		public String LargePhotoFileName { get; set; }
					
		[Field("ThumbnailPhotoFileName", FieldType.NVARCHAR)]
		public String ThumbnailPhotoFileName { get; set; }
	
	#endregion Instance Properties

    }
}
