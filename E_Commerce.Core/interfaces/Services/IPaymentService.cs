using E_Commerce.Core.DTO;
using E_Commerce.Core.DTO.OrderDto;

namespace E_Commerce.Core.interfaces.Services
{
    public interface IPaymentService
    {

        public Task<CustomerCartDto> CreateOrUpdateIntentForExistingOrder(CustomerCartDto input);
        public Task<CustomerCartDto> CreateOrUpdateIntentForNewOrder(string cartId);
        public Task<OrderResultDto> UpdatePaymentStatusFailed(string paymentIntentId);
        public Task<OrderResultDto> UpdatePaymentStatusSucceeded(string paymentIntentId);

    }
}
