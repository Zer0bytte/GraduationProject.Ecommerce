using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Ecommerce.Application.Features.Orders.Queries.User.GetOrderPriceDetails;

public sealed class GetOrderPriceDetailsResponse
{
    public decimal SubTotal { get; set; }
}
