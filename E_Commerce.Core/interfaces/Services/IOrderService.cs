
using E_Commerce.Core.DTO.OrderDto;
using E_Commerce.Core.Models.Order_Aggregation;
namespace E_Commerce.Core.interfaces.Services
{
    public interface IOrderService
    {
        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync();
        public Task<OrderResultDto?> CreateOrderAsync(OrderDto order);
        public Task<OrderResultDto> GetOrderAsync(int id, string email);
        public Task<IEnumerable<OrderResultDto>> GetAllOrdersAsync(string email);



    }
}
