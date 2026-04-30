using BuildingBlocks.CQRS;
using CatalogApi.Exceptions;
using CatalogApi.Models;
using Marten;

namespace CatalogApi.Products.UpdateProduct
{
    public record UpdateProductCommand
        (Guid Id, string Name, List<string> Category, string ImageFile, decimal Price):ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsSuccess);
    internal class UpdateProductHandler(IDocumentSession session,ILogger<UpdateProductHandler> logger) : 
        ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling UpdateProductCommand for product id: {Id}", request.Id);
            var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
            if (product == null)
            {
                throw new ProductNotFoundException();
            }
            product.Name = request.Name;
            product.Category = request.Category;
            product.ImageFile = request.ImageFile;
            product.Price = request.Price;
            product.Category= request.Category;
            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }
    }
}
