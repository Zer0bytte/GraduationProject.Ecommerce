namespace Ecommerce.Application.Features.Orders.Queries.GetOrderDetails;

public record GetOrderDetailsQuery : IRequest<GetOrderDetailsResult>
{
    public Guid OrderId { get; set; }
}
