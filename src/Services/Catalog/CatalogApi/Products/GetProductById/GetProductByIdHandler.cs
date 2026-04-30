using BuildingBlocks.CQRS;
using CatalogApi.Exceptions;
using CatalogApi.Models;
using Marten;

namespace CatalogApi.Products.GetProductById
{
   public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    public class GetProductByIdHandler(IDocumentSession session,ILogger<GetProductByIdHandler> logger)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetProductByIdQuery for Product Id: {ProductId}", request.Id);
            var product =await session.LoadAsync<Product>(request.Id,cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundException();
            }
            return new GetProductByIdResult(product);
        }
    }
}
