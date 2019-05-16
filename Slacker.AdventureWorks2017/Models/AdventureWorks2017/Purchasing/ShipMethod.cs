
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Purchasing  {

    [Table("ShipMethod", "PurchShipM")]
    public class ShipMethod : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("ShipMethodID", FieldType.INT)]
		public Int32 ShipMethodId { get; set; }
					
		[Field("ShipBase", FieldType.MONEY)]
		public Decimal ShipBase { get; set; }
					
		[Field("ShipRate", FieldType.MONEY)]
		public Decimal ShipRate { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("Name", FieldType.NVARCHAR)]
		public String Name { get; set; }
	
	#endregion Instance Properties

    }
}
