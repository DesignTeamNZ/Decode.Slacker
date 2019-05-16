
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Sales  {

    [Table("SpecialOfferProduct", "SalesSpeci")]
    public class SpecialOfferProduct : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("SpecialOfferID", FieldType.INT)]
		public Int32 SpecialOfferId { get; set; }
					
		[Field("ProductID", FieldType.INT)]
		public Int32 ProductId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
	
	#endregion Instance Properties

    }
}
