
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.HumanResources  {

    [Table("EmployeePayHistory", "HumanEmplo")]
    public class EmployeePayHistory : DataModel {
	#region Instance Properties
					
		[Field("PayFrequency", FieldType.TINY_INT)]
		public Int32 PayFrequency { get; set; }
					
		[Field("BusinessEntityID", FieldType.INT)]
		public Int32 BusinessEntityId { get; set; }
					
		[Field("Rate", FieldType.MONEY)]
		public Decimal Rate { get; set; }
					
		[Field("RateChangeDate", FieldType.DATETIME)]
		public DateTime RateChangeDate { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
	
	#endregion Instance Properties

    }
}
