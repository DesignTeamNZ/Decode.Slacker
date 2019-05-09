
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Production  {

    [Table("ProductReview", "ProduProdu")]
    public class ProductReview : DataModel {
	#region Instance Properties
					
		[Field("Rating", FieldType.INT)]
		public Int32 Rating { get; set; }
					
		[Field("ProductReviewID", FieldType.INT)]
		public Int32 ProductReviewId { get; set; }
					
		[Field("ProductID", FieldType.INT)]
		public Int32 ProductId { get; set; }
					
		[Field("ReviewDate", FieldType.DATETIME)]
		public DateTime ReviewDate { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("EmailAddress", FieldType.NVARCHAR)]
		public String EmailAddress { get; set; }
					
		[Field("Comments", FieldType.NVARCHAR)]
		public String Comments { get; set; }
					
		[Field("ReviewerName", FieldType.NVARCHAR)]
		public String ReviewerName { get; set; }
	
	#endregion Instance Properties

    }
}
