using BuildingBlocks.CQRS;
using CatalogApi.Exceptions;
using CatalogApi.Models;
using Marten;

namespace CatalogApi.Products.DeleteProduct
{

    public record DeleteProductCommand(Guid ProductId) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool Success);
    public class DeleteProductHandler(IDocumentSession session,ILogger<DeleteProductHandler> logger)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling DeleteProductCommand for product id: {Id}", request.ProductId);
            
            session.Delete<Product>(request.ProductId);
             await session.SaveChangesAsync(cancellationToken);
            return new DeleteProductResult(true);
        }
    }
}
