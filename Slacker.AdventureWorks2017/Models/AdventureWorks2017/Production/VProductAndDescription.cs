
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Production  {

    [Table("vProductAndDescription", "ProduVProd")]
    public class VProductAndDescription : DataModel {
	#region Instance Properties
					
		[Field("ProductID", FieldType.INT)]
		public Int32 ProductId { get; set; }
					
		[Field("Description", FieldType.NVARCHAR)]
		public String Description { get; set; }
					
		[Field("CultureID", FieldType.NCHAR)]
		public String CultureId { get; set; }
					
		[Field("Name", FieldType.NVARCHAR)]
		public String Name { get; set; }
					
		[Field("ProductModel", FieldType.NVARCHAR)]
		public String ProductModel { get; set; }
	
	#endregion Instance Properties

    }
}
