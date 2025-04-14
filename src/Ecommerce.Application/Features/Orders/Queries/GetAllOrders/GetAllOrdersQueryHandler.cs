namespace Ecommerce.Application.Features.Orders.Queries.GetAllOrders;

public class GetAllOrdersQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAllOrdersQuery, PagedResult<GetAllOrdersResult>>
{
    public async Task<PagedResult<GetAllOrdersResult>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken)
    {
        DbSet<Order> source = context.Orders;

        IQueryable<GetAllOrdersResult> orders = source.Select(x => new GetAllOrdersResult
        {
            OrderId = x.Id,
            BuyerEmail = x.BuyerEmail,
            OrderDate = x.OrderDate,
            PaymentMethod = x.PaymentMethod,
            PaymentStatus = x.PaymentStatus,
            //ShippingPrice = x.DeliveryMethod.Price,
            SubTotal = x.SubTotal,
            Status = x.Status
        })
            .Skip((query.Page - 1) * query.Limit)
            .Take(query.Limit);

        int total = await source.CountAsync();

        return PagedResult<GetAllOrdersResult>.Create(query, total, orders);
    }
}
