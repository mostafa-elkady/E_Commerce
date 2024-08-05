using E_Commerce.Core.DTO;
using E_Commerce.Core.Models;

namespace E_Commerce.Core.interfaces.Repositories
{
    public interface ICartRepository
    {
        // Get Cart
        Task<CustomerCartDto?> GetCart(string CartId);

        // Create Or Update Cart
        Task<CustomerCartDto?> UpdateCartAsync(CustomerCartDto customerCart);
        //Delete Cart
        Task<bool> DeleteCart(string CartId);

    }
}
