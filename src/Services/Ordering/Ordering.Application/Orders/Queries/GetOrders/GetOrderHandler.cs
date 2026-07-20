
namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrderHandler(IAppDbContext context)
        : IQueryHandler<GetOrderQuery, GetOrderResult>
    {
        public async Task<GetOrderResult> Handle(GetOrderQuery request, CancellationToken cancellationToken)

        {
            var index = request.PaginationRequest.PageIndex;
            var size = request.PaginationRequest.PageSize;

            var totalCount = await context.Orders.LongCountAsync(cancellationToken);
            var orders =await context.Orders
                .Include(o=>o.OrderItems)
                .OrderBy(o => o.OrderName.Value)
                .Skip(index * size)
                .Take(size)
                .ToListAsync(cancellationToken);

            return new GetOrderResult(
                new PaginatedResult<OrderDto>(index,
                size,
                totalCount
                , orders.ToOrderDtoList()));
        }
    }
}
