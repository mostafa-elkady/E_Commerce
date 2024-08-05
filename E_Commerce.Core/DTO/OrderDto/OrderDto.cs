

namespace E_Commerce.Core.DTO.OrderDto
{
    public class OrderDto
    {
        public string BuyerEmail { get; set; }
        public string CartId { get; set; }
        public int? DeliveryMethodId { get; set; }
        public AddressDto ShippingAddress { get; set; }
    }
}
