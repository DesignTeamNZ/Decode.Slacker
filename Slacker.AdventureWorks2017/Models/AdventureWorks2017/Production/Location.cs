
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Production  {

    [Table("Location", "ProduLocat")]
    public class Location : DataModel {
	#region Instance Properties
					
		[Field("LocationID", FieldType.SMALL_INT)]
		public Int16 LocationId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("Availability", FieldType.DECIMAL)]
		public Decimal Availability { get; set; }
					
		[Field("CostRate", FieldType.SMALL_MONEY)]
		public Decimal CostRate { get; set; }
					
		[Field("Name", FieldType.NVARCHAR)]
		public String Name { get; set; }
	
	#endregion Instance Properties

    }
}
