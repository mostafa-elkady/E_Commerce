using E_Commerce.Core.DTO.OrderDto;
using E_Commerce.Core.interfaces.Services;
using E_Commerce.Core.Models.Order_Aggregation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E_Commerce.Api.Controllers
{

    public class OrdersController : APIBaseController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<OrderResultDto>> Create(OrderDto orderDto)
        {
            var order = await _orderService.CreateOrderAsync(orderDto);
            return Ok(order);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OrderResultDto>>> GetAllOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _orderService.GetAllOrdersAsync(email);
            return Ok(orders);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResultDto>> GetOrder(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetOrderAsync(id, email);
            return Ok(order);
        }

        [HttpGet("DeliverMethod")]
        public async Task<ActionResult<IEnumerable<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderService.GetDeliveryMethodAsync());
        }
    }
}
