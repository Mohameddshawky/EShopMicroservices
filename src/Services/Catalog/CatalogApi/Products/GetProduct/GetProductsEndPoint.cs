using Carter;
using CatalogApi.Models;
using Mapster;
using MediatR;

namespace CatalogApi.Products.GetProduct
{

    public record GetProductResponse(IEnumerable<Product> Products);
    public class GetProductsEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
            {
                var query = new GetProductsQuery();
                var res = await sender.Send(query);
                var response = res.Adapt<GetProductResponse>();
                return Results.Ok(response);
            }).WithName("GetProducts")
            .Produces<GetProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get all products");
        }
    }
}
