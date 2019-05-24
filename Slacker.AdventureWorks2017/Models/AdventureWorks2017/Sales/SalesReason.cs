
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Sales  {

    [Table("SalesReason", "SalesSales")]
    public class SalesReason : DataModel {
	#region Instance Properties
					
		[Field("SalesReasonID", FieldType.INT)]
		public Int32 SalesReasonId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("Name", FieldType.NVARCHAR)]
		public String Name { get; set; }
					
		[Field("ReasonType", FieldType.NVARCHAR)]
		public String ReasonType { get; set; }
	
	#endregion Instance Properties

    }
}
