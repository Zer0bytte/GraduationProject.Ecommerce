namespace Ecommerce.Application.Features.Suppliers.Queries.GetAllSuppliers;

public class GetAllSuppliersQuery : PagedQuery, IRequest<PagedResult<GetAllSuppliersResult>>
{
    public bool? IsVerified { get; set; }

}
