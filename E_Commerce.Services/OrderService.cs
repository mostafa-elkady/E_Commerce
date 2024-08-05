using AutoMapper;
using E_Commerce.Core;
using E_Commerce.Core.DTO.OrderDto;
using E_Commerce.Core.interfaces.Repositories;
using E_Commerce.Core.interfaces.Services;
using E_Commerce.Core.Models;
using E_Commerce.Core.Models.Order_Aggregation;
using E_Commerce.Core.Specifications;
using E_Commerce.repository.Data;
namespace E_Commerce.Services
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        private readonly ICartRepository _cartRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentService _paymentService;
        public OrderService(
            IMapper mapper,
            ICartRepository cartRepository,
            IPaymentService paymentService, StoreContext context,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _cartRepository = cartRepository;
            _paymentService = paymentService;
            _unitOfWork = unitOfWork;
        }
        // Method To Create Order
        public async Task<OrderResultDto?> CreateOrderAsync(OrderDto input)
        {
            // 1. Get Cart
            var cart = await _cartRepository.GetCart(input.CartId) ?? throw new Exception($"Cart With Id {input.CartId} is Not Found");
            // 3.Create Order Items List outside For Loop To use it and Fill it Inside Loop
            var OrderItems = new List<OrderItem>();
            foreach (var CartItem in cart.Items)
            {
                // 3.1.Get The Product
                var Product = await _unitOfWork.Repository<Product>().GetByIdAsync(CartItem.Id);
                if (Product is null) continue;
                // 3.2 Create The Product Item
                var ProductItem = new ProductItemOrdered
                {
                    PictureUrl = Product.PictureUrl,
                    ProductId = Product.Id,
                    ProductName = Product.Name
                };
                // 3.3 Create The Order Item
                var OrderItem = new OrderItem
                {
                    Product = ProductItem,
                    Price = Product.Price,
                    Quantity = CartItem.Quantity
                };

                OrderItems.Add(OrderItem);
            }
            if (!OrderItems.Any()) throw new Exception("Cart Items Not Found");
            // 4. Get The Delivery Method
            if (!input.DeliveryMethodId.HasValue) throw new Exception("No Delivery Method Was Selected");
            var Delivery = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(input.DeliveryMethodId.Value) ?? throw new Exception("Invalid Delivery Method Id");

            // 5.Shipping Address
            var ShippingAddress = _mapper.Map<Address>(input.ShippingAddress);

            // 6 Calculate The Sub Total
            var SubTotal = OrderItems.Sum(item => item.Price * item.Quantity);
            // 7. Get The PaymentIntentId
            var specs = new OrderWithPaymentIntentIdSpecification(cart.PaymentIntentId);
            var existingOrder =await _unitOfWork.Repository<Order>().GetWithSpecsAsync(specs);

            if (existingOrder is not null)
            {
                _unitOfWork.Repository<Order>().Delete(existingOrder);
                await _paymentService.CreateOrUpdateIntentForExistingOrder(cart);
            }
            else
            {
                // Reassign cart to the updated cart
                cart = await _paymentService.CreateOrUpdateIntentForNewOrder(cart.Id);
            }

            // 8. Create The Order
            var Order = new Order
            {
                BuyerEmail = input.BuyerEmail,
                DeliveryMethod = Delivery,
                ShippingAddress = ShippingAddress,
                Items = OrderItems,
                SubTotal = SubTotal,
                PaymentIntentId = cart.PaymentIntentId,
                CartId = cart.Id
            };
            // 9. Add Order To Database
            await _unitOfWork.Repository<Order>().AddAsync(Order);
            var Result = await _unitOfWork.CompleteAsync();
            if (Result <= 0) return null;
            // 10 return The order as order Result Dto
            return _mapper.Map<OrderResultDto?>(Order);
        }

        // Method To Get All Orders
        public async Task<IEnumerable<OrderResultDto>> GetAllOrdersAsync(string email)
        {
            var specs = new OrderSpecification(email);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecsAsync(specs);
            if (!orders.Any()) throw new Exception($"No Orders yet For user with email => {email}");
            return _mapper.Map<IEnumerable<OrderResultDto>>(orders);
        }

        // Method To Get  Order
        public async Task<OrderResultDto> GetOrderAsync(int id, string email)
        {
            var specs = new OrderSpecification(email);
            var order = await _unitOfWork.Repository<Order>().GetWithSpecsAsync(specs);
            return order is null ? throw new Exception($"No Orders yet For user with id => {id}") : _mapper.Map<OrderResultDto>(order);
        }
        // Method To Get Delivery Methods
        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync() => await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();

    }
}
