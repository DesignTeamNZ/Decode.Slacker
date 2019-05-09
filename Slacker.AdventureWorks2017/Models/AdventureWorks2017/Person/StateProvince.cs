
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Person  {

    [Table("StateProvince", "PersoState")]
    public class StateProvince : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("TerritoryID", FieldType.INT)]
		public Int32 TerritoryId { get; set; }
					
		[Field("StateProvinceID", FieldType.INT)]
		public Int32 StateProvinceId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("CountryRegionCode", FieldType.NVARCHAR)]
		public String CountryRegionCode { get; set; }
					
		[Field("StateProvinceCode", FieldType.NCHAR)]
		public String StateProvinceCode { get; set; }
					
		[Field("IsOnlyStateProvinceFlag", FieldType.BIT)]
		public Boolean IsOnlyStateProvinceFlag { get; set; }
					
		[Field("Name", FieldType.NVARCHAR)]
		public String Name { get; set; }
	
	#endregion Instance Properties

    }
}
