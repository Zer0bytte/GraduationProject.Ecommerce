namespace Ecommerce.Application.Features.Orders.Queries.GetOrderDetails;

public class GetOrderDetailsQueryHandler(IApplicationDbContext context, ICurrentUser ICurrentUser) : IRequestHandler<GetOrderDetailsQuery, GetOrderDetailsResult>
{
    public async Task<GetOrderDetailsResult> Handle(GetOrderDetailsQuery query, CancellationToken cancellationToken)
    {
        GetOrderDetailsResult? orderDetails = await context.Orders
            .Where(o => o.Id == query.OrderId && o.UserId == ICurrentUser.Id)
            .Select(o => new GetOrderDetailsResult
            {
                OrderId = o.Id,
                OrderDate = o.OrderDate,
                DeliveryMethod = o.DeliveryMethod.ShortName,
                PaymentMethod = o.PaymentMethod,
                PaymentStatus = o.PaymentStatus,
                ShippingAddress = new OrderDetailsAddress
                {
                    FirstName = o.Address.FirstName,
                    LastName = o.Address.LastName,
                    City = o.Address.City,
                    Governorate = o.Address.Governorate,
                    Street = o.Address.Street,
                },
                Status = o.Status,
                SubTotal = o.SubTotal,
                ShippingPrice = o.DeliveryMethod.Price,
                OrderItems = o.OrderItems.Select(oi => new OrderDetailItems
                {
                    ProductId = oi.ProductId,
                    ImageUrl = oi.ImageUrl,
                    Price = oi.Price,
                    ProductName = oi.ProductName,
                    Quantity = oi.Quantity,
                    SupplierId = oi.SupplierId,
                    SupplierName = oi.Supplier != null ? oi.Supplier.StoreName : null,
                    Status = oi.Status
                }).ToList(),
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (orderDetails is null) 
            throw new NotFoundException("Order", query.OrderId);

        return orderDetails;
    }
}
