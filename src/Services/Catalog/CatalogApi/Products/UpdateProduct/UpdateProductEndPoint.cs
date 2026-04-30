using Carter;
using Mapster;
using MediatR;

namespace CatalogApi.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, List<string> Category   , string Description, string ImageFile, decimal Price);
    public record UpdateProductResponse(bool IsSuccess);
    public class UpdateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                var command =  request.Adapt<UpdateProductCommand>();
                var res = await sender.Send(command);
                var response =res.Adapt<UpdateProductResponse>();
                return Results.Ok(response);
            }).WithName("UpdateProduct")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithTags("Products")
            .WithDescription("Update a product in the catalog");
        }
    }
}
