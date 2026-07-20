


namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrdersByNameHandler(IAppDbContext context)
        : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
    {
        public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery request, CancellationToken cancellationToken)
        {

            var orders=await context.Orders
                .Include(o=>o.OrderItems)
                .AsNoTracking()
                .Where(o => o.OrderName.Value.Contains(request.Name))
                .OrderBy(o =>o.OrderName)
                .ToListAsync(cancellationToken);

            
            return new GetOrdersByNameResult((orders.ToOrderDtoList()));
        }

       
    }
}
