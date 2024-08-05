namespace E_Commerce.Core.DTO
{
    public class ProductToReturnDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public string productBrand { get; set; }
        public int TypeId { get; set; }
        public string productType { get; set; }
    }
}
