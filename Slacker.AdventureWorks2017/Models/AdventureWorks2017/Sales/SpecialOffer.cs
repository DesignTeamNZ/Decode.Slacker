
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Sales  {

    [Table("SpecialOffer", "SalesSpeci")]
    public class SpecialOffer : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("SpecialOfferID", FieldType.INT)]
		public Int32 SpecialOfferId { get; set; }
					
		[Field("MinQty", FieldType.INT)]
		public Int32 MinQty { get; set; }
					
		[Field("MaxQty", FieldType.INT)]
		public Int32? MaxQty { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("StartDate", FieldType.DATETIME)]
		public DateTime StartDate { get; set; }
					
		[Field("EndDate", FieldType.DATETIME)]
		public DateTime EndDate { get; set; }
					
		[Field("DiscountPct", FieldType.SMALL_MONEY)]
		public Decimal DiscountPct { get; set; }
					
		[Field("Type", FieldType.NVARCHAR)]
		public String Type { get; set; }
					
		[Field("Category", FieldType.NVARCHAR)]
		public String Category { get; set; }
					
		[Field("Description", FieldType.NVARCHAR)]
		public String Description { get; set; }
	
	#endregion Instance Properties

    }
}
