
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Production  {

    [Table("BillOfMaterials", "ProduBillO")]
    public class BillOfMaterials : DataModel {
	#region Instance Properties
					
		[Field("BOMLevel", FieldType.SMALL_INT)]
		public Int16 BomlEvel { get; set; }
					
		[Field("BillOfMaterialsID", FieldType.INT)]
		public Int32 BillOfMaterialsId { get; set; }
					
		[Field("ProductAssemblyID", FieldType.INT)]
		public Int32? ProductAssemblyId { get; set; }
					
		[Field("ComponentID", FieldType.INT)]
		public Int32 ComponentId { get; set; }
					
		[Field("StartDate", FieldType.DATETIME)]
		public DateTime StartDate { get; set; }
					
		[Field("EndDate", FieldType.DATETIME)]
		public DateTime? EndDate { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("PerAssemblyQty", FieldType.DECIMAL)]
		public Decimal PerAssemblyQty { get; set; }
					
		[Field("UnitMeasureCode", FieldType.NCHAR)]
		public String UnitMeasureCode { get; set; }
	
	#endregion Instance Properties

    }
}
