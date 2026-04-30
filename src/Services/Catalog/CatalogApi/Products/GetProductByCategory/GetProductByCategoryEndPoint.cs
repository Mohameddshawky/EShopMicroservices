using Carter;
using CatalogApi.Models;
using Mapster;
using MediatR;
namespace CatalogApi.Products.GetProductByCategory

{

    public record GetProductByCategoryResponse(IEnumerable<Product> Products);
    public class GetProductByCategoryEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var query = new GetProductByCategoryQuery(category);
                var res = await sender.Send(query);
                var response = res.Adapt<GetProductByCategoryResponse>();
                return Results.Ok(response);
            }).WithName("GetProductByCategory")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithTags("Products")
            .WithDescription("Get products by category");

        }
    }
}
