
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Sales  {

    [Table("SalesTerritoryHistory", "SalesSales")]
    public class SalesTerritoryHistory : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("BusinessEntityID", FieldType.INT)]
		public Int32 BusinessEntityId { get; set; }
					
		[Field("TerritoryID", FieldType.INT)]
		public Int32 TerritoryId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("StartDate", FieldType.DATETIME)]
		public DateTime StartDate { get; set; }
					
		[Field("EndDate", FieldType.DATETIME)]
		public DateTime? EndDate { get; set; }
	
	#endregion Instance Properties

    }
}
