using BuildingBlocks.CQRS;
using CatalogApi.Models;
using FluentValidation;
using Marten;

namespace CatalogApi.Products.DeleteProduct
{

    public record DeleteProductCommand(Guid ProductId) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool Success);

    public class  DeleteProductCommandValidator:AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("Product id is required.");
        }
    }

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
