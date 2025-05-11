namespace Ecommerce.Application.Features.Orders.Queries.Admin.GetAllOrders;

public class GetAllOrdersQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAllOrdersQuery, PagedResult<GetAllOrdersResult>>
{
    public async Task<PagedResult<GetAllOrdersResult>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken)
    {
        IQueryable<Order> source = context.Orders.Where(o => o.Status == query.Status).OrderByDescending(o => o.OrderDate);

        IQueryable<GetAllOrdersResult> orders = source.Select(x => new GetAllOrdersResult
        {
            OrderId = x.Id,
            BuyerEmail = x.User.FullName,
            OrderDate = x.OrderDate,
            PaymentMethod = x.PaymentMethod,
            PaymentStatus = x.PaymentStatus,
            Status = x.Status
        })
            .Skip((query.Page - 1) * query.Limit)
            .Take(query.Limit);

        int total = await source.CountAsync();

        return PagedResult<GetAllOrdersResult>.Create(query, total, orders);
    }
}
