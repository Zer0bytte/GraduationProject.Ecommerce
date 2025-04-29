namespace Ecommerce.Application.Features.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetAllProductsQuery, PagedResult<GetAllProductsResult>>
{
    public async Task<PagedResult<GetAllProductsResult>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
    {
        HttpRequest? httpRequest = httpContextAccessor.HttpContext?.Request;
        string imageUrl = httpRequest?.Scheme + "://" + httpRequest?.Host + "/media/";


        IQueryable<Product> baseQuery = context.Products.AsQueryable();
        if (!string.IsNullOrWhiteSpace(query.SearchQuery))
            baseQuery = baseQuery.Where(prd => prd.Title.Contains(query.SearchQuery));

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

        if(query.CategoryId.HasValue && query.CategoryId != Guid.Empty)
        {
            baseQuery = baseQuery.Where(p => p.CategoryId == query.CategoryId);
        }

        var options = baseQuery
         .SelectMany(p => p.Options)
         .GroupBy(o => o.OptionGroupName)
         .ToList();
        IQueryable<GetAllProductsResult> source = baseQuery.Select(p => new GetAllProductsResult
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
            Category = p.Category.Name
        });

        IQueryable<GetAllProductsResult> products = source
        .Skip((query.Page - 1) * query.Limit)
        .Take(query.Limit);

        int total = await source.CountAsync(cancellationToken: cancellationToken);

        return PagedResult<GetAllProductsResult>.Create(query, total, products);
    }
}