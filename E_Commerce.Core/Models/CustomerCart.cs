namespace E_Commerce.Core.Models
{
    public class CustomerCart
    {

        public CustomerCart(string id)
        {
            Id = id;
        }
        public string Id { get; set; }
        public IEnumerable<CartItem> Items { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal ShippingPrice { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
    }
}
