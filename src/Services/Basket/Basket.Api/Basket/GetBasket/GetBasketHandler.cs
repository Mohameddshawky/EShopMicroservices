namespace Basket.Api.Basket.GetBasket
{
    public class GetBasketHandler
    {

        public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
        public record GetBasketResult(ShoppingCart Cart);

        public class GetBasketQueryHandler(IBasketRepository repository)
            : IQueryHandler<GetBasketQuery, GetBasketResult>
        {
            public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
            {
                var basket = await repository.GetBasketAsync(query.UserName);

                return new GetBasketResult(basket);
            }
        }
    }
}
