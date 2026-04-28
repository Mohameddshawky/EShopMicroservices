using BuildingBlocks.CQRS;
using CatalogApi.Models;
using Marten;

namespace CatalogApi.Products.GetProduct
{

    public record GetProductsQuery() : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);
    internal class GetProductsHandler(IDocumentSession session,ILogger<GetProductsHandler> logger) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetProductsQuery");
            var products =await session.Query<Product>().ToListAsync();

            return new GetProductsResult(products);
        }
    }
}
