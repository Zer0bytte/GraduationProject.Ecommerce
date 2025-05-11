namespace Ecommerce.Application.Features.Orders.Queries.Supplier.GetSupplierOrderItems;

public class GetSupplierOrderItemsQuery : PagedQuery, IRequest<PagedResult<GetSupplierOrderItemsResult>>
{
    public OrderItemStatus? Status { get; set; }
}