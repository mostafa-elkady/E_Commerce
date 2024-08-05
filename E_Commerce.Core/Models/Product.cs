namespace E_Commerce.Core.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }        // Represent Of navigational property
        public ProductBrand productBrand { get; set; } // Navigational property for Relation
        public int TypeId { get; set; }
        public ProductType productType { get; set; }
    }
}
