using Domain;
using Domain.Contracts;
using StackExchange.Redis;
using System.Text.Json;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer _connectionMultiplexer) : IBasketRepository
    {
        private readonly IDatabase _database = _connectionMultiplexer.GetDatabase();    
        public async Task<Basket?> GetBasketAsync(string username)
        {
            var data = await _database.StringGetAsync(username);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Basket>(data);
        }
        public async Task<Basket?> UpdateBasketAsync(Basket basket, TimeSpan? timeToLive = null)
        {
            var created = await _database.StringSetAsync(
            basket.Username,
            JsonSerializer.Serialize(basket),
            timeToLive ?? TimeSpan.FromDays(30)
            );
            return !created ? null : await GetBasketAsync(basket.Username);
        }
        public async Task<bool> DeleteBasketAsync(string username)
            => await _database.KeyDeleteAsync(username);       
    }
}
