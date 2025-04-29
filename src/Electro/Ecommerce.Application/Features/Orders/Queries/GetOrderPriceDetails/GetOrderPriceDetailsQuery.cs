namespace Ecommerce.Application.Features.Orders.Queries.GetOrderPriceDetails;
public record GetOrderPriceDetailsQuery : IRequest<GetOrderPriceDetailsResponse>
{
    public Guid AddressId { get; set; }
    public string CartId { get; set; } = default!;


}