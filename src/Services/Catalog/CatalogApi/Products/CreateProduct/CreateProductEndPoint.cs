using Carter;
using Mapster;
using MediatR;

namespace CatalogApi.Products.CreateProduct
{

    public record CreateProductRequest(
        string Name,
        string Description,
        decimal Price,
        List<string> Category,
        string ImageFile
    );
    public record CreateProductResponse(Guid Id);
    public class CreateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();
                var res = await sender.Send(command);
                var response = res.Adapt<CreateProductResponse>();
                return Results.Created($"/products/{response.Id}",response);
            }).WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(statusCode: StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create a new product with the provided details.");




        }
    }
}
