using Ecommerce.Application.Features.Cart.Queries.GetCart;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Application.Features.Orders.Queries.GetOrderPriceDetails;

internal sealed class GetOrderPriceDetailsQueryHandler(IApplicationDbContext context, ISender sender, IShippingCalculatorService shippingCalculatorService)
    : IRequestHandler<GetOrderPriceDetailsQuery, GetOrderPriceDetailsResponse>
{
    public async Task<GetOrderPriceDetailsResponse> Handle(GetOrderPriceDetailsQuery request, CancellationToken cancellationToken)
    {
        GetCartResult cart = await sender.Send(new GetCartQuery() { Id = request.CartId });
        var address = await context.Addresses.FindAsync(request.AddressId);
        if (address is null) throw new NotFoundException("Address", request.AddressId);
        decimal subTotal = 0;
        decimal shippingPrice = 0;
        Dictionary<Guid, decimal> suppliers = new();
        foreach (CartItemDto item in cart.Cart.CartItems)
        {
            Product? product = await context.Products.Include(p => p.Supplier).FirstOrDefaultAsync();
            subTotal += product.Price * item.Quantity;

            if (product.SupplierId.HasValue)
            {
                if (!suppliers.ContainsKey(product.SupplierId.Value))
                {
                    suppliers.Add(product.SupplierId.Value, GetSupplierShippingPrice(address!.Governorate, product.Supplier.Governorate));
                }
            }
        }

        foreach (decimal sp in suppliers.Values)
        {
            shippingPrice += sp;
        }

        return new GetOrderPriceDetailsResponse
        {
            SubTotal = subTotal,
            ShippingPrice = shippingPrice
        };
    }

    private decimal GetSupplierShippingPrice(Governorate destination, Governorate source)
    {
        var result = shippingCalculatorService.CalculateShippingPrice(destination.ToString(), source.ToString());
        return result;
    }
}