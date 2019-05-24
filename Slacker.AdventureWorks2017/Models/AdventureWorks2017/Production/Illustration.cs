
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Production  {

    [Table("Illustration", "ProduIllus")]
    public class Illustration : DataModel {
	#region Instance Properties
					
		[Field("IllustrationID", FieldType.INT)]
		public Int32 IllustrationId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("Diagram", FieldType.XML)]
		public String Diagram { get; set; }
	
	#endregion Instance Properties

    }
}
