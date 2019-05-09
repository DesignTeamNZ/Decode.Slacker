
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slacker;

namespace AdventureWorks2017.Production  {

    [Table("Product", "ProduProdu")]
    public class Product : DataModel {
	#region Instance Properties
					
		[Field("rowguid", FieldType.UNIQUE_IDENTIFIER)]
		public String Rowguid { get; set; }
					
		[Field("SafetyStockLevel", FieldType.SMALL_INT)]
		public Int16 SafetyStockLevel { get; set; }
					
		[Field("ReorderPoint", FieldType.SMALL_INT)]
		public Int16 ReorderPoint { get; set; }
					
		[Field("ProductID", FieldType.INT)]
		public Int32 ProductId { get; set; }
					
		[Field("DaysToManufacture", FieldType.INT)]
		public Int32 DaysToManufacture { get; set; }
					
		[Field("ProductSubcategoryID", FieldType.INT)]
		public Int32? ProductSubcategoryId { get; set; }
					
		[Field("ProductModelID", FieldType.INT)]
		public Int32? ProductModelId { get; set; }
					
		[Field("StandardCost", FieldType.MONEY)]
		public Decimal StandardCost { get; set; }
					
		[Field("ListPrice", FieldType.MONEY)]
		public Decimal ListPrice { get; set; }
					
		[Field("ModifiedDate", FieldType.DATETIME)]
		public DateTime ModifiedDate { get; set; }
					
		[Field("SellStartDate", FieldType.DATETIME)]
		public DateTime SellStartDate { get; set; }
					
		[Field("SellEndDate", FieldType.DATETIME)]
		public DateTime? SellEndDate { get; set; }
					
		[Field("DiscontinuedDate", FieldType.DATETIME)]
		public DateTime? DiscontinuedDate { get; set; }
					
		[Field("Weight", FieldType.DECIMAL)]
		public Decimal? Weight { get; set; }
					
		[Field("Color", FieldType.NVARCHAR)]
		public String Color { get; set; }
					
		[Field("Size", FieldType.NVARCHAR)]
		public String Size { get; set; }
					
		[Field("ProductNumber", FieldType.NVARCHAR)]
		public String ProductNumber { get; set; }
					
		[Field("SizeUnitMeasureCode", FieldType.NCHAR)]
		public String SizeUnitMeasureCode { get; set; }
					
		[Field("WeightUnitMeasureCode", FieldType.NCHAR)]
		public String WeightUnitMeasureCode { get; set; }
					
		[Field("ProductLine", FieldType.NCHAR)]
		public String ProductLine { get; set; }
					
		[Field("Class", FieldType.NCHAR)]
		public String Class { get; set; }
					
		[Field("Style", FieldType.NCHAR)]
		public String Style { get; set; }
					
		[Field("MakeFlag", FieldType.BIT)]
		public Boolean MakeFlag { get; set; }
					
		[Field("FinishedGoodsFlag", FieldType.BIT)]
		public Boolean FinishedGoodsFlag { get; set; }
					
		[Field("Name", FieldType.NVARCHAR)]
		public String Name { get; set; }
	
	#endregion Instance Properties

    }
}
