
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Production  {

    [Table("vProductModelCatalogDescription", "ProduVProd")]
    public class VProductModelCatalogDescription : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("ProductModelID", FieldType.INT)]
		public Int32 ProductModelId { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("Summary", FieldType.NVARCHAR)]
		public String Summary { get; set; }
					
		[Field("Manufacturer", FieldType.NVARCHAR)]
		public String Manufacturer { get; set; }
					
		[Field("Copyright", FieldType.NVARCHAR)]
		public String Copyright { get; set; }
					
		[Field("ProductURL", FieldType.NVARCHAR)]
		public String ProductUrl { get; set; }
					
		[Field("WarrantyPeriod", FieldType.NVARCHAR)]
		public String WarrantyPeriod { get; set; }
					
		[Field("WarrantyDescription", FieldType.NVARCHAR)]
		public String WarrantyDescription { get; set; }
					
		[Field("NoOfYears", FieldType.NVARCHAR)]
		public String NoOfYears { get; set; }
					
		[Field("MaintenanceDescription", FieldType.NVARCHAR)]
		public String MaintenanceDescription { get; set; }
					
		[Field("Wheel", FieldType.NVARCHAR)]
		public String Wheel { get; set; }
					
		[Field("Saddle", FieldType.NVARCHAR)]
		public String Saddle { get; set; }
					
		[Field("Pedal", FieldType.NVARCHAR)]
		public String Pedal { get; set; }
					
		[Field("BikeFrame", FieldType.NVARCHAR)]
		public String BikeFrame { get; set; }
					
		[Field("Crankset", FieldType.NVARCHAR)]
		public String Crankset { get; set; }
					
		[Field("PictureAngle", FieldType.NVARCHAR)]
		public String PictureAngle { get; set; }
					
		[Field("PictureSize", FieldType.NVARCHAR)]
		public String PictureSize { get; set; }
					
		[Field("ProductPhotoID", FieldType.NVARCHAR)]
		public String ProductPhotoId { get; set; }
					
		[Field("Material", FieldType.NVARCHAR)]
		public String Material { get; set; }
					
		[Field("Color", FieldType.NVARCHAR)]
		public String Color { get; set; }
					
		[Field("ProductLine", FieldType.NVARCHAR)]
		public String ProductLine { get; set; }
					
		[Field("Style", FieldType.NVARCHAR)]
		public String Style { get; set; }
					
		[Field("RiderExperience", FieldType.NVARCHAR)]
		public String RiderExperience { get; set; }
					
		[Field("Name", FieldType.NVARCHAR)]
		public String Name { get; set; }
	
	#endregion Instance Properties

    }
}
