namespace Ecommerce.Application.Features.Orders.Queries.GetSupplierOrderItems;

public class GetSupplierOrderItemsQuery : PagedQuery, IRequest<PagedResult<GetSupplierOrderItemsResult>>
{
    public OrderItemStatus? Status { get; set; }
}