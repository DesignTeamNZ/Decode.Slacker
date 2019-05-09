
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Sales  {

    [Table("vStoreWithAddresses", "SalesVStor")]
    public class VStoreWithAddresses : DataModel {
	#region Instance Properties
					
		[Field("BusinessEntityID", FieldType.INT)]
		public Int32 BusinessEntityId { get; set; }
					
		[Field("PostalCode", FieldType.NVARCHAR)]
		public String PostalCode { get; set; }
					
		[Field("AddressLine1", FieldType.NVARCHAR)]
		public String AddressLine1 { get; set; }
					
		[Field("AddressLine2", FieldType.NVARCHAR)]
		public String AddressLine2 { get; set; }
					
		[Field("City", FieldType.NVARCHAR)]
		public String City { get; set; }
					
		[Field("StateProvinceName", FieldType.NVARCHAR)]
		public String StateProvinceName { get; set; }
					
		[Field("CountryRegionName", FieldType.NVARCHAR)]
		public String CountryRegionName { get; set; }
					
		[Field("Name", FieldType.NVARCHAR)]
		public String Name { get; set; }
					
		[Field("AddressType", FieldType.NVARCHAR)]
		public String AddressType { get; set; }
	
	#endregion Instance Properties

    }
}
