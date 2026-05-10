namespace Basket.Api.Data
{
    public class CacheBasketRepository
        (
        IBasketRepository innerRepository
        ,IDistributedCache cache
        ) 
        : IBasketRepository
    {
        public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            await innerRepository.DeleteBasketAsync(userName, cancellationToken);
            await cache.RemoveAsync(userName, cancellationToken);
            return true;
        }

        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);
            if (!string.IsNullOrEmpty(cachedBasket))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            }
            var basket = await innerRepository.GetBasketAsync(userName, cancellationToken);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            var result = await innerRepository.StoreBasketAsync(basket, cancellationToken);
            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(result), cancellationToken);
            return result;
        }
    }
}
