using E_Commerce.Core.DTO;
using E_Commerce.Core.interfaces.Repositories;
using StackExchange.Redis;
using System.Text.Json;

namespace E_Commerce.repository
{
    public class CartRepository : ICartRepository
    {
        private readonly IDatabase _database;

        public CartRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        //Delete
        public async Task<bool> DeleteCart(string CartId)
        {
            return await _database.KeyDeleteAsync(CartId);
        }
        //Get
        public async Task<CustomerCartDto?> GetCart(string CartId)
        {
            var Cart = await _database.StringGetAsync(CartId);
            return Cart.IsNull ? null : JsonSerializer.Deserialize<CustomerCartDto>(Cart);
        }
        //Create Or Update
        public async Task<CustomerCartDto?> UpdateCartAsync(CustomerCartDto customerCart)
        {
            var JsonCart = JsonSerializer.Serialize(customerCart);
            var CreatedOrUpdated = await _database.StringSetAsync(customerCart.Id, JsonCart, TimeSpan.FromDays(1));
            if (!CreatedOrUpdated) return null;
            return await GetCart(customerCart.Id);


        }

    }
}
