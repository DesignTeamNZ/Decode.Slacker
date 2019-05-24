
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Sales  {

    [Table("SalesOrderHeaderSalesReason", "SalesSales")]
    public class SalesOrderHeaderSalesReason : DataModel {
	#region Instance Properties
					
		[Field("SalesOrderID", FieldType.INT)]
		public Int32 SalesOrderId { get; set; }
					
		[Field("SalesReasonID", FieldType.INT)]
		public Int32 SalesReasonId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
	
	#endregion Instance Properties

    }
}
