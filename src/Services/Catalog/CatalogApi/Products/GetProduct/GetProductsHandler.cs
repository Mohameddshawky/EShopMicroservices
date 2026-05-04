using BuildingBlocks.CQRS;
using CatalogApi.Models;
using Marten;
using Marten.Pagination;

namespace CatalogApi.Products.GetProduct
{

    public record GetProductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);
    internal class GetProductsHandler(IDocumentSession session) : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products =await session.Query<Product>()
                .ToPagedListAsync(request.PageNumber ?? 1, request.PageSize ?? 10, cancellationToken);

            return new GetProductsResult(products);
        }
    }
}
