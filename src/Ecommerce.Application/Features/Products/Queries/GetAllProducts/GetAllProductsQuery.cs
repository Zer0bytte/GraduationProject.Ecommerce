namespace Ecommerce.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQuery : PagedQuery, IRequest<PagedResult<GetAllProductsResult>>
{
    public string? SearchQuery { get; set; }
}
