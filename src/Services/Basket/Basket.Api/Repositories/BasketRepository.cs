using Basket.Api.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Basket.Api.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCashe;
        public BasketRepository(IDistributedCache redisCashe)
        {
            _redisCashe = redisCashe;
        }
        public async Task<ShoppingCart> GetBasket(string username)
        {
            var basket = await _redisCashe.GetStringAsync(username);

            if (string.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }
        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redisCashe.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));

            return await GetBasket(basket.UserName);
        }

        public async Task DeleteBasket(string username)
        {
            await _redisCashe.RemoveAsync(username);
        }



    }
}
