namespace Basket.Api.Data
{
    public interface IBasketRepository
    {
        public Task<ShoppingCart> GetBasketAsync(string userName,CancellationToken cancellationToken=default);
        public Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket,CancellationToken cancellationToken=default );
        
        public Task<bool> DeleteBasketAsync(string userName,CancellationToken cancellationToken=default);
    }
}
