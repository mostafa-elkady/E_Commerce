using E_Commerce.Core.Models.Order_Aggregation;

namespace E_Commerce.Core.DTO.OrderDto
{
    public class OrderResultDto
    {
        public string BuyerEmail { get; set; }
        public string PaymentIntentId { get; set; }
        public string CartId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public AddressDto ShippingAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal GetTotal { get; set; }
    }
}
