namespace E_Commerce.Core.DTO
{
    public class CustomerCartDto
    {
        public CustomerCartDto(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
        public IEnumerable<CartItemDto> Items { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
