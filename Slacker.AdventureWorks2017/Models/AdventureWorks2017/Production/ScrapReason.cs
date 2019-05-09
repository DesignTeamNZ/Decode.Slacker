
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Production  {

    [Table("ScrapReason", "ProduScrap")]
    public class ScrapReason : DataModel {
	#region Instance Properties
					
		[Field("ScrapReasonID", FieldType.SMALL_INT)]
		public Int16 ScrapReasonId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("Name", FieldType.NVARCHAR)]
		public String Name { get; set; }
	
	#endregion Instance Properties

    }
}
