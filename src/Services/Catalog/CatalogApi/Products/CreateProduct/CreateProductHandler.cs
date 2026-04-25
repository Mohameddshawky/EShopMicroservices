using BuildingBlocks.CQRS;
using CatalogApi.Models;
using Marten;


namespace CatalogApi.Products.CreateProduct
{

    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        :ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);
    internal  class CreateProductCommandHandler(IDocumentSession session) :
        ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
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
