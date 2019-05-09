
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.HumanResources  {

    [Table("vJobCandidate", "HumanVJobC")]
    public class VJobCandidate : DataModel {
	#region Instance Properties
					
		[Field("JobCandidateID", FieldType.INT)]
		public Int32 JobCandidateId { get; set; }
					
		[Field("BusinessEntityID", FieldType.INT)]
		public Int32? BusinessEntityId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("Name.Prefix", FieldType.NVARCHAR)]
		public String NamePrefix { get; set; }
					
		[Field("Name.First", FieldType.NVARCHAR)]
		public String NameFirst { get; set; }
					
		[Field("Name.Middle", FieldType.NVARCHAR)]
		public String NameMiddle { get; set; }
					
		[Field("Name.Last", FieldType.NVARCHAR)]
		public String NameLast { get; set; }
					
		[Field("Name.Suffix", FieldType.NVARCHAR)]
		public String NameSuffix { get; set; }
					
		[Field("Skills", FieldType.NVARCHAR)]
		public String Skills { get; set; }
					
		[Field("Addr.Type", FieldType.NVARCHAR)]
		public String AddrType { get; set; }
					
		[Field("Addr.Loc.CountryRegion", FieldType.NVARCHAR)]
		public String AddrLocCountryRegion { get; set; }
					
		[Field("Addr.Loc.State", FieldType.NVARCHAR)]
		public String AddrLocState { get; set; }
					
		[Field("Addr.Loc.City", FieldType.NVARCHAR)]
		public String AddrLocCity { get; set; }
					
		[Field("Addr.PostalCode", FieldType.NVARCHAR)]
		public String AddrPostalCode { get; set; }
					
		[Field("EMail", FieldType.NVARCHAR)]
		public String EmAil { get; set; }
					
		[Field("WebSite", FieldType.NVARCHAR)]
		public String WebSite { get; set; }
	
	#endregion Instance Properties

    }
}
