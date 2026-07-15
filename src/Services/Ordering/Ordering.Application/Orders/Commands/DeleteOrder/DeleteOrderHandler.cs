
namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderHandler(IAppDbContext dbContext)
        : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
           var orderId = OrderId.Of(request.Id);
            var order = await dbContext.Orders.FindAsync(orderId, cancellationToken);
            if (order == null)
            {
                throw new OrderNotFoundException(request.Id);
            }
            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new DeleteOrderResult(true);
        }
    }
}
