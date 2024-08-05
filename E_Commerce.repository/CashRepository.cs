using E_Commerce.Core.interfaces.Repositories;
using StackExchange.Redis;
using System.Text.Json;

namespace E_Commerce.repository
{
    public class CashRepository : ICashRepository
    {
        private readonly IDatabase _database;

        public CashRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<string?> GetCashResponseAsync(string key)
        {
            var response = await _database.StringGetAsync(key);
            return response.IsNullOrEmpty ? null : response.ToString();
        }

        public async Task SetCashResponseAsync(string key, object response, TimeSpan time)
        {
            var JsonCashResponse = JsonSerializer
                .Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            await _database.StringSetAsync(key, JsonCashResponse, time);
        }
    }
}
