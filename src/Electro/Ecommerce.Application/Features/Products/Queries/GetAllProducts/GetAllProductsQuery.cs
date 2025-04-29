namespace Ecommerce.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQuery : PagedQuery, IRequest<PagedResult<GetAllProductsResult>>
{
    public string? SearchQuery { get; set; }
    public decimal? MinimumPrice { get; set; }
    public decimal? MaximumPrice { get; set; }
    public bool? HasDiscount { get; set; }
    public Guid? CategoryId { get; set; }

}
