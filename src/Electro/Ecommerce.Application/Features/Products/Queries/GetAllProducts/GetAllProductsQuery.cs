using Ecommerce.Application.Common.Persistance.Cursor;

namespace Ecommerce.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQuery : CursorQuery, IRequest<CursorResult<GetAllProductsResult>>
{
    public string? SearchQuery { get; set; }
    public decimal? MinimumPrice { get; set; }
    public decimal? MaximumPrice { get; set; }
    public bool? HasDiscount { get; set; }
    public Guid? CategoryId { get; set; }

    public string? OptionGroupName { get; set; }
    public string? OptionValue { get; set; }

}
