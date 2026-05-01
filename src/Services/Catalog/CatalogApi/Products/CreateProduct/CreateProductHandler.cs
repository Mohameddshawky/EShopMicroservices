using BuildingBlocks.CQRS;
using CatalogApi.Models;
using FluentValidation;
using Marten;


namespace CatalogApi.Products.CreateProduct
{

    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        :ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);


    public class CrateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CrateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Category).NotEmpty().WithMessage("At least one category is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image file is required.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }
    internal  class CreateProductCommandHandler(IDocumentSession session,ILogger<CreateProductCommandHandler> logger) :
        ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
           logger.LogInformation("Handling CreateProductCommand for product: {ProductName}", command.Name);
            var product = new Product
            {
                Name = command.Name,
                ImageFile = command.ImageFile,
                Category = command.Category,
                Description = command.Description,
                Price = command.Price
            };

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);



        }
    }
}
