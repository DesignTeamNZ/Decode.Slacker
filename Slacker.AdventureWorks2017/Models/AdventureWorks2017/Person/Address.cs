
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Person  {

    [Table("Address", "PersoAddre")]
    public class Address : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("StateProvinceID", FieldType.INT)]
		public Int32 StateProvinceId { get; set; }
					
		[Field("AddressID", FieldType.INT)]
		public Int32 AddressId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("SpatialLocation", FieldType.GEOGRAPHY)]
		public String SpatialLocation { get; set; }
					
		[Field("PostalCode", FieldType.NVARCHAR)]
		public String PostalCode { get; set; }
					
		[Field("AddressLine1", FieldType.NVARCHAR)]
		public String AddressLine1 { get; set; }
					
		[Field("AddressLine2", FieldType.NVARCHAR)]
		public String AddressLine2 { get; set; }
					
		[Field("City", FieldType.NVARCHAR)]
		public String City { get; set; }
	
	#endregion Instance Properties

    }
}
