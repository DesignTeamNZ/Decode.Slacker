
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.HumanResources  {

    [Table("JobCandidate", "HumanJobCa")]
    public class JobCandidate : DataModel {
	#region Instance Properties
					
		[Field("JobCandidateID", FieldType.INT)]
		public Int32 JobCandidateId { get; set; }
					
		[Field("BusinessEntityID", FieldType.INT)]
		public Int32? BusinessEntityId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("Resume", FieldType.XML)]
		public String Resume { get; set; }
	
	#endregion Instance Properties

    }
}
