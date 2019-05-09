
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Production  {

    [Table("vProductModelInstructions", "ProduVProd")]
    public class VProductModelInstructions : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("LocationID", FieldType.INT)]
		public Int32? LocationId { get; set; }
					
		[Field("ProductModelID", FieldType.INT)]
		public Int32 ProductModelId { get; set; }
					
		[Field("LotSize", FieldType.INT)]
		public Int32? LotSize { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("SetupHours", FieldType.DECIMAL)]
		public Decimal? SetupHours { get; set; }
					
		[Field("MachineHours", FieldType.DECIMAL)]
		public Decimal? MachineHours { get; set; }
					
		[Field("LaborHours", FieldType.DECIMAL)]
		public Decimal? LaborHours { get; set; }
					
		[Field("Step", FieldType.NVARCHAR)]
		public String Step { get; set; }
					
		[Field("Instructions", FieldType.NVARCHAR)]
		public String Instructions { get; set; }
					
		[Field("Name", FieldType.NVARCHAR)]
		public String Name { get; set; }
	
	#endregion Instance Properties

    }
}
