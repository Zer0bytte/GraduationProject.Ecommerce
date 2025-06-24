using Ecommerce.Application.Common.Persistance.Cursor;
using MassTransit.Internals;

namespace Ecommerce.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler(IApplicationDbContext context, HostingConfig hostingConfig)
    : IRequestHandler<GetAllProductsQuery, CursorResult<GetAllProductsResult>>
{
    public async Task<CursorResult<GetAllProductsResult>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
    {
        string imageUrl = hostingConfig.HostName + "/media/";

       

        var baseQuery = context.Products.AsQueryable();
        if (!string.IsNullOrWhiteSpace(query.SearchQuery))
            baseQuery = baseQuery.Where(prd => prd.Title.Contains(query.SearchQuery)
            || prd.Tags.Contains(query.SearchQuery) || prd.Description.Contains(query.SearchQuery));

        if (query.HasDiscount.HasValue && query.HasDiscount.Value)
        {
            baseQuery = baseQuery.Where(prd => prd.Discount > 0);
        }

        if (query.MinimumPrice.HasValue)
        {
            baseQuery = baseQuery.Where(prd =>
                prd.Price * (1 - prd.Discount / 100m) >= query.MinimumPrice.Value);
        }

        if (query.MaximumPrice.HasValue)
        {
            baseQuery = baseQuery.Where(prd =>
                prd.Price * (1 - prd.Discount / 100m) <= query.MaximumPrice.Value);
        }

        if (query.CategoryId.HasValue && query.CategoryId != Guid.Empty)
        {
            baseQuery = baseQuery.Where(p => p.CategoryId == query.CategoryId);
        }

        if (query.SupplierId.HasValue && query.SupplierId != Guid.Empty)
        {
            baseQuery = baseQuery.Where(p => p.SupplierId == query.SupplierId);
        }
        if (!string.IsNullOrWhiteSpace(query.Cursor))
        {
            Cursor? decodedCursor = Cursor.Decode(query.Cursor);
            if (decodedCursor is not null)
            {
                baseQuery = baseQuery.Where(x => x.CreatedOn < decodedCursor.Date || x.CreatedOn == decodedCursor.Date && x.Id <= decodedCursor.LastId);
            }
        }

        List<GetAllProductsResult> products = await baseQuery
            .OrderByDescending(x => x.CreatedOn)
            .ThenByDescending(x => x.Id)
            .Select(p => new GetAllProductsResult
            {
                Id = p.Id,
                SupplierId = p.Supplier.UserId,
                SupplierName = p.Supplier.StoreName,
                Title = p.Title,
                Price = p.Price,
                DiscountedPrice = p.Discount >= 1 ? p.Price * (1 - p.Discount / 100m) : 0,
                DiscountPercentage = p.Discount,
                Description = p.Description,
                Images = p.Images.Select(p => imageUrl + p.NameOnServer).ToArray(),
                Category = p.Category.Name,
                CreatedOn = p.CreatedOn,
            }).Take(query.Limit + 1).ToListAsync(cancellationToken);

        List<GetAllProductsResult> prdFinal = products.Take(query.Limit).ToList();

        bool hasMore = products.Count > query.Limit;
        DateTime? nextDate = products.Count > query.Limit ? products[^1].CreatedOn : null;
        Guid? nextId = products.Count > query.Limit ? products[^1].Id : null;

        CursorResult<GetAllProductsResult> result = new CursorResult<GetAllProductsResult>
        {
            Items = prdFinal,
            Cursor = nextDate is not null && nextId is not null ? Cursor.Encode(nextDate.Value, nextId.Value) : null,
            HasMore = hasMore
        };

        return result;


    }


}