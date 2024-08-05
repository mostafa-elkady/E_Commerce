using AutoMapper;
using E_Commerce.Core.DTO;
using E_Commerce.Core.DTO.OrderDto;
using E_Commerce.Core.interfaces.Repositories;
using E_Commerce.Core.interfaces.Services;
using Product = E_Commerce.Core.Models.Product;
using E_Commerce.Core.Models.Order_Aggregation;
using Microsoft.Extensions.Configuration;
using Stripe;
using E_Commerce.Core.Specifications;
using E_Commerce.Core;

namespace E_Commerce.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public PaymentService(
            IMapper mapper, 
            ICartRepository cartRepository, 
            IUnitOfWork unitOfWork, 
            IConfiguration configuration)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<CustomerCartDto> CreateOrUpdateIntentForExistingOrder(CustomerCartDto customerCart)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            //1 Calculate amount
            foreach (var item in customerCart.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                if (product.Price != item.Price) item.Price = product.Price;
            }

            var total = customerCart.Items.Sum(item => item.Price * item.Quantity);
            if (!customerCart.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method Selected");
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(customerCart.DeliveryMethodId.Value);
            var shippingPrice = deliveryMethod.Cost;
            long amount = (long)((total * 100) + (shippingPrice * 100));
            //2. Create Or Update 
            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrWhiteSpace(customerCart.PaymentIntentId))
            {
                // Create 
                var options = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                paymentIntent = await service.CreateAsync(options);
                customerCart.PaymentIntentId = paymentIntent.Id;
                customerCart.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                // Update
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = amount,
                };
                await service.UpdateAsync(customerCart.PaymentIntentId, options);
            }

            // Final Update The Cart
            await _cartRepository.UpdateCartAsync(customerCart);

            return customerCart;
        }

        public async Task<CustomerCartDto> CreateOrUpdateIntentForNewOrder(string cartId)
        {
            var customerCart = await _cartRepository.GetCart(cartId);
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            //1 Calculate amount
            foreach (var item in customerCart.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                if (product.Price != item.Price) item.Price = product.Price;
            }

            var total = customerCart.Items.Sum(item => item.Price * item.Quantity);
            if (!customerCart.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method Selected");
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(customerCart.DeliveryMethodId.Value);
            var shippingPrice = deliveryMethod.Cost;
            customerCart.ShippingPrice = deliveryMethod.Cost;
            long amount = (long)((total * 100) + (shippingPrice * 100));
            //2. Create Or Update 
            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;
            if (string.IsNullOrWhiteSpace(customerCart.PaymentIntentId))
            {
                // Create 
                var options = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                paymentIntent = await service.CreateAsync(options);
                customerCart.PaymentIntentId = paymentIntent.Id;
                customerCart.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                // Update
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = amount,
                };
                await service.UpdateAsync(customerCart.PaymentIntentId, options);
            }

            // Final Update The Cart
            await _cartRepository.UpdateCartAsync(customerCart);

            return customerCart;
        }

        public async Task<OrderResultDto> UpdatePaymentStatusFailed(string paymentIntentId)
        {
            var specs = new OrderWithPaymentIntentIdSpecification(paymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetWithSpecsAsync(specs) ?? throw new Exception($"No Order with PaymentIntentId {paymentIntentId}");
            order.Status = OrderStatus.PaymentFailed;
            await _unitOfWork.Repository<Order>().AddAsync(order);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<OrderResultDto>(order);
        }

        public async Task<OrderResultDto> UpdatePaymentStatusSucceeded(string paymentIntentId)
        {
            var specs = new OrderWithPaymentIntentIdSpecification(paymentIntentId);
            var order = await _unitOfWork.Repository<Order>().GetWithSpecsAsync(specs) ?? throw new Exception($"No Order with PaymentIntentId {paymentIntentId}");
            order.Status = OrderStatus.PaymentReceived;
            await _unitOfWork.Repository<Order>().AddAsync(order);
            await _unitOfWork.CompleteAsync();
            await _cartRepository.DeleteCart(order.CartId);
            return _mapper.Map<OrderResultDto>(order);
        }
    }
}
