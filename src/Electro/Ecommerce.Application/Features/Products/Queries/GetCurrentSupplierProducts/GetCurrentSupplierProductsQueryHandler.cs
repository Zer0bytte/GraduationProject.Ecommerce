using Ecommerce.Application.Common.Persistance.Cursor;
using MassTransit.Internals;

namespace Ecommerce.Application.Features.Products.Queries.GetAllProducts;

public class GetCurrentSupplierProductsQueryHandler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ICurrentUser currentUser)
    : IRequestHandler<GetCurrentSupplierProductsQuery, CursorResult<GetCurrentSupplierProductsResult>>
{
    public async Task<CursorResult<GetCurrentSupplierProductsResult>> Handle(GetCurrentSupplierProductsQuery query, CancellationToken cancellationToken)
    {
        HttpRequest? httpRequest = httpContextAccessor.HttpContext?.Request;
        string imageUrl = httpRequest?.Scheme + "://" + httpRequest?.Host + "/media/";

        IQueryable<Product> baseQuery = context.Products.Where(p => p.SupplierId == currentUser.SupplierId).AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.SearchQuery))
            baseQuery = baseQuery.Where(prd => prd.Title.Contains(query.SearchQuery));

        if (query.HasDiscount.HasValue && query.HasDiscount.Value)
        {
            baseQuery = baseQuery.Where(prd => prd.Discount > 0);
        }


        if (query.CategoryId.HasValue && query.CategoryId != Guid.Empty)
        {
            baseQuery = baseQuery.Where(p => p.CategoryId == query.CategoryId);
        }

        if (!string.IsNullOrWhiteSpace(query.Cursor))
        {
            Cursor? decodedCursor = Cursor.Decode(query.Cursor);
            if (decodedCursor is not null)
            {
                baseQuery = baseQuery.Where(x => x.CreatedOn < decodedCursor.Date || x.CreatedOn == decodedCursor.Date && x.Id <= decodedCursor.LastId);
            }
        }
        List<GetCurrentSupplierProductsResult> products = await baseQuery
            .OrderByDescending(x => x.CreatedOn)
            .ThenByDescending(x => x.Id)
            .Select(p => new GetCurrentSupplierProductsResult
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                Images = p.Images.Select(p => imageUrl + p.NameOnServer).ToArray(),
                Category = p.Category.Name,
                CreatedOn = p.CreatedOn,
                Stock = p.Stock,
                BoughtCount = context.OrderItems.Where(oi => oi.ProductId == p.Id).Sum(oi => oi.Quantity)
            }).Take(query.Limit + 1).ToListAsync(cancellationToken);

        var prdFinal = products.Take(query.Limit).ToList();

        bool hasMore = products.Count > query.Limit;
        DateTime? nextDate = products.Count > query.Limit ? products[^1].CreatedOn : null;
        Guid? nextId = products.Count > query.Limit ? products[^1].Id : null;

        CursorResult<GetCurrentSupplierProductsResult> result = new CursorResult<GetCurrentSupplierProductsResult>
        {
            Items = prdFinal,
            Cursor = nextDate is not null && nextId is not null ? Cursor.Encode(nextDate.Value, nextId.Value) : null,
            HasMore = hasMore
        };

        return result;


    }
}