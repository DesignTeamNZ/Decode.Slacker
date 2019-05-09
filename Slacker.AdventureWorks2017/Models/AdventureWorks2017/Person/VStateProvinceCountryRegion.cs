
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Person  {

    [Table("vStateProvinceCountryRegion", "PersoVStat")]
    public class VStateProvinceCountryRegion : DataModel {
	#region Instance Properties
					
		[Field("StateProvinceID", FieldType.INT)]
		public Int32 StateProvinceId { get; set; }
					
		[Field("TerritoryID", FieldType.INT)]
		public Int32 TerritoryId { get; set; }
					
		[Field("CountryRegionCode", FieldType.NVARCHAR)]
		public String CountryRegionCode { get; set; }
					
		[Field("StateProvinceCode", FieldType.NCHAR)]
		public String StateProvinceCode { get; set; }
					
		[Field("IsOnlyStateProvinceFlag", FieldType.BIT)]
		public Boolean IsOnlyStateProvinceFlag { get; set; }
					
		[Field("StateProvinceName", FieldType.NVARCHAR)]
		public String StateProvinceName { get; set; }
					
		[Field("CountryRegionName", FieldType.NVARCHAR)]
		public String CountryRegionName { get; set; }
	
	#endregion Instance Properties

    }
}
