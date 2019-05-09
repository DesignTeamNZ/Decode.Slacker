
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Sales  {

    [Table("Customer", "SalesCusto")]
    public class Customer : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("CustomerID", FieldType.INT)]
		public Int32 CustomerId { get; set; }
					
		[Field("PersonID", FieldType.INT)]
		public Int32? PersonId { get; set; }
					
		[Field("StoreID", FieldType.INT)]
		public Int32? StoreId { get; set; }
					
		[Field("TerritoryID", FieldType.INT)]
		public Int32? TerritoryId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("AccountNumber", FieldType.VARCHAR)]
		public String AccountNumber { get; set; }
	
	#endregion Instance Properties

    }
}
