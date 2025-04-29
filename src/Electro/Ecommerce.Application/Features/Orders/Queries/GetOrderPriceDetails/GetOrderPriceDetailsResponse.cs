using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Ecommerce.Application.Features.Orders.Queries.GetOrderPriceDetails;

public sealed class GetOrderPriceDetailsResponse
{
    public decimal SubTotal { get; set; }
    public decimal ShippingPrice { get; set; }
}
