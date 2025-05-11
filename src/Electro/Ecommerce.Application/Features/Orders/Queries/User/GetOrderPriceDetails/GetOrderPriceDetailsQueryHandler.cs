using Ecommerce.Application.Features.Cart.Queries.GetCart;

namespace Ecommerce.Application.Features.Orders.Queries.User.GetOrderPriceDetails;

internal sealed class GetOrderPriceDetailsQueryHandler(IApplicationDbContext context, ISender sender, IShippingCalculatorService shippingCalculatorService)
    : IRequestHandler<GetOrderPriceDetailsQuery, GetOrderPriceDetailsResponse>
{
    public async Task<GetOrderPriceDetailsResponse> Handle(GetOrderPriceDetailsQuery request, CancellationToken cancellationToken)
    {
        GetCartResult cart = await sender.Send(new GetCartQuery() { Id = request.CartId });
        Domain.Entities.Address? address = await context.Addresses.FindAsync(request.AddressId);
        if (address is null) throw new NotFoundException("Address", request.AddressId);
        decimal subTotal = 0;
        foreach (CartItemDto item in cart.Cart.CartItems)
        {
            Product? product = await context.Products.FirstOrDefaultAsync(prd => prd.Id == item.Id);
            subTotal += product.Price * item.Quantity;
        }


        return new GetOrderPriceDetailsResponse
        {
            SubTotal = subTotal,
        };
    }

}