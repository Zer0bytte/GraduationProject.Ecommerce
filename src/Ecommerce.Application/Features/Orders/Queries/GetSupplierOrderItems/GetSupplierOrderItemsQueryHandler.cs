namespace Ecommerce.Application.Features.Orders.Queries.GetSupplierOrderItems;

public class GetSupplierOrderItemsQueryHandler : IRequestHandler<GetSupplierOrderItemsQuery, PagedResult<GetSupplierOrderItemsResult>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUser _currentUser;

    public GetSupplierOrderItemsQueryHandler(IApplicationDbContext context, ICurrentUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<PagedResult<GetSupplierOrderItemsResult>> Handle(GetSupplierOrderItemsQuery query, CancellationToken cancellationToken)
    {
        if (_currentUser.SupplierId == Guid.Empty)
            throw new UnauthorizedAccessException("User is not a supplier");

        IQueryable<OrderItem> baseQuery = _context.OrderItems
            .Include(oi => oi.Order)
                .ThenInclude(o => o.User)
            .Include(oi => oi.Order)
                .ThenInclude(o => o.Address)
            .Where(oi => oi.SupplierId == _currentUser.SupplierId);

        if (query.Status.HasValue)
        {
            baseQuery = baseQuery.Where(oi => oi.Status == query.Status.Value);
        }

        int total = await baseQuery.CountAsync(cancellationToken);

        List<GetSupplierOrderItemsResult> items = await baseQuery
            .Skip((query.Page - 1) * query.Limit)
            .Take(query.Limit)
            .Select(oi => new GetSupplierOrderItemsResult
            {
                OrderItemId = oi.Id,
                OrderId = oi.OrderId,
                ProductId = oi.ProductId,
                ProductName = oi.ProductName,
                ImageUrl = oi.ImageUrl,
                Price = oi.Price,
                Quantity = oi.Quantity,
                Status = oi.Status,
                OrderDate = oi.Order.OrderDate,
                BuyerEmail = oi.Order.User.Email,
                BuyerName = oi.Order.User.FullName,
                ShippingAddress = $"{oi.Order.Address.Street}, {oi.Order.Address.City}, {oi.Order.Address.Governorate}"
            })
            .ToListAsync(cancellationToken);

        return PagedResult<GetSupplierOrderItemsResult>.Create(query, total, items);
    }
}