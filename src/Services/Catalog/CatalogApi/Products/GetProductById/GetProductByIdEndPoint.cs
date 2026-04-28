using Carter;
using CatalogApi.Exceptions;
using CatalogApi.Models;
using Mapster;
using MediatR;

namespace CatalogApi.Products.GetProductById
{

    public record GetProductByIdResponse(Product Product);
    public class GetProductByIdEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id:guid}", async (Guid id, ISender sender) =>
            {
                var query = new GetProductByIdQuery(id);
                var res = await sender.Send(query);
               
                var response = res.Adapt<GetProductByIdResponse>();
                return Results.Ok(response);
            }).WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithSummary("Get a product by its ID")
            .WithDescription("Retrieves a product from the catalog by its unique identifier. Returns the product details if found, or a 404 Not Found status if the product does not exist."); 
        }
    }
}
