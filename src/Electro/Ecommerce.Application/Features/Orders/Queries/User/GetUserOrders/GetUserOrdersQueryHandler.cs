namespace Ecommerce.Application.Features.Orders.Queries.User.GetUserOrders;

public class GetUserOrdersQueryHandler(IApplicationDbContext context, ICurrentUser ICurrentUser)
    : IRequestHandler<GetUserOrdersQuery, PagedResult<GetUserOrdersResult>>
{
    public async Task<PagedResult<GetUserOrdersResult>> Handle(GetUserOrdersQuery query, CancellationToken cancellationToken)
    {
        IQueryable<Order> source = context.Orders.Where(x => x.UserId == ICurrentUser.Id).OrderBy(o => o.Status == OrderStatus.Cancelled);


        IQueryable<GetUserOrdersResult> orders = source.Select(o => new GetUserOrdersResult
        {
            OrderId = o.Id,
            OrderDate = o.OrderDate,
            PaymentMethod = o.PaymentMethod,
            PaymentStatus = o.PaymentStatus,
            SubTotal = o.SubTotal,
            ShipTo = o.Address.FirstName + " " + o.Address.LastName,
            OrderItems = o.OrderItems.Select(oi => new OrderItemResult
            {
                OrderItemId = oi.Id,
                ProductId = oi.ProductId,
                ProductName = oi.ProductName,
                ImageUrl = oi.ImageUrl,
                Status = oi.Status

            }).ToList()
        })
            .Skip((query.Page - 1) * query.Limit)
            .Take(query.Limit);


        int total = await source.CountAsync();

        return PagedResult<GetUserOrdersResult>.Create(query, total, orders);
    }
}
