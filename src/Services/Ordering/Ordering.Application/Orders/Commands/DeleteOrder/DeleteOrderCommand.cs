
namespace Ordering.Application.Orders.Commands
{
    public record DeleteOrderCommand(Guid Id) : ICommand<DeleteOrderResult>;
    public record DeleteOrderResult(bool IsSuccess);
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("OrderId is required");
        }
    }
}
