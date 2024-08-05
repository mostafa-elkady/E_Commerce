using AutoMapper;
using E_Commerce.Api.Errors;
using E_Commerce.Core.DTO;
using E_Commerce.Core.interfaces.Repositories;
using E_Commerce.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{

    public class CartController : APIBaseController
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartController(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        // Get Or ReCreate Cart
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerCartDto>> GetCustomerCart(string id)
        {
            var Cart = await _cartRepository.GetCart(id);
            return Cart is null ? new CustomerCartDto(id) : Cart;
        }
        // Update or Create New Cart
        [HttpPost]
        public async Task<ActionResult<CustomerCartDto>> GetCustomerCart(CustomerCart cart)
        {

            var mappedCart = _mapper.Map<CustomerCartDto>(cart);
            var CreatedOrUpdatedCart = await _cartRepository.UpdateCartAsync(mappedCart);
            if (CreatedOrUpdatedCart is null) return BadRequest(new ApiResponse(400));
            return Ok(CreatedOrUpdatedCart);
        }
        //Delete Cart
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteCart(string id)
        {
            return await _cartRepository.DeleteCart(id);
        }

    }
}
