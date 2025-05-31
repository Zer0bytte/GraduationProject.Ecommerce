namespace Ecommerce.Application.Features.Suppliers.Queries.GetCurrentSupplier;
public class GetCurrentSupplierQueryHandler(ICurrentUser currentUser, IApplicationDbContext context)
    : IRequestHandler<GetCurrentSupplierQuery, GetCurrentSupplierResult>
{
    public async Task<GetCurrentSupplierResult> Handle(GetCurrentSupplierQuery request, CancellationToken cancellationToken)
    {
        SupplierProfile? supplier = await context.SupplierProfiles.FindAsync(currentUser.SupplierId);
        if (supplier is null) throw new NotFoundException("Supplier");

        var ordersCount = await context.OrderItems.Where(oi => oi.SupplierId == supplier.Id).CountAsync();
        var itemsSoldCount = await context.OrderItems.Where(oi => oi.SupplierId == supplier.Id).SumAsync(oi => oi.Quantity);


        var salesChart = context.OrderItems.GroupBy(oi => oi.Product.Category.Name).Select(g => new SalesChartItem
        {
            CategoryName = g.Key,
            Count = context.OrderItems.Where(oi=>oi.SupplierId == supplier.Id && oi.Product.Category.Name == g.Key).Sum(oi=>oi.Quantity)
        })
            .ToList();

        return new GetCurrentSupplierResult
        {
            Id = supplier.Id,
            BusinessName = supplier.BusinessName,
            StoreName = supplier.StoreName,
            VerificationStatus = supplier.VerificationStatus,
            Balance = supplier.Balance,
            SalesChart = salesChart,
            ItemsSoldCount = itemsSoldCount,
            OrdersCount = ordersCount
        };
    }
}