namespace Ecommerce.Application.Features.Orders.Queries.GetUserOrders;

public class GetUserOrdersQueryHandler(IApplicationDbContext context, ICurrentUser ICurrentUser)
    : IRequestHandler<GetUserOrdersQuery, PagedResult<GetUserOrdersResult>>
{
    public async Task<PagedResult<GetUserOrdersResult>> Handle(GetUserOrdersQuery query, CancellationToken cancellationToken)
    {
        IQueryable<Order> source = context.Orders.Where(x => x.UserId == ICurrentUser.Id);


        IQueryable<GetUserOrdersResult> orders = source.Select(o => new GetUserOrdersResult
        {
            OrderId = o.Id,
            OrderDate = o.OrderDate,
            PaymentMethod = o.PaymentMethod,
            PaymentStatus = o.PaymentStatus,
            //ShippingPrice = o.DeliveryMethod.Price,
            Status = o.Status,
            SubTotal = o.SubTotal,
        }).Skip((query.Page - 1) * query.Limit)
            .Take(query.Limit);


        int total = await source.CountAsync();

        return PagedResult<GetUserOrdersResult>.Create(query, total, orders);
    }
}
