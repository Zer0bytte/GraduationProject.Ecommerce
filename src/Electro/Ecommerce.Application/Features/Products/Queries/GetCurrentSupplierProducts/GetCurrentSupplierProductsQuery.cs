using Ecommerce.Application.Common.Persistance.Cursor;

namespace Ecommerce.Application.Features.Products.Queries.GetAllProducts;

public class GetCurrentSupplierProductsQuery : CursorQuery, IRequest<CursorResult<GetCurrentSupplierProductsResult>>
{
    public string? SearchQuery { get; set; }
    public bool? HasDiscount { get; set; }
    public Guid? CategoryId { get; set; }

}
