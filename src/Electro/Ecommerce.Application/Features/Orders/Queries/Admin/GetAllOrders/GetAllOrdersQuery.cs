namespace Ecommerce.Application.Features.Orders.Queries.Admin.GetAllOrders;

public class GetAllOrdersQuery : PagedQuery, IRequest<PagedResult<GetAllOrdersResult>>
{
    public OrderStatus Status { get; set; }
}
