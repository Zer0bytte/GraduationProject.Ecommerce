namespace Ecommerce.Application.Features.Products.Queries.GetMostSoldProducts;
public class GetMostSoldProductsQueryHandler(IApplicationDbContext context, HostingConfig hostingConfig) :
    IRequestHandler<GetMostSoldProductsQuery, List<GetMostSoldProductsResult>>
{
    public async Task<List<GetMostSoldProductsResult>> Handle(GetMostSoldProductsQuery request, CancellationToken cancellationToken)
    {
        string imageUrl = hostingConfig.HostName + "/media/";
        List<GetMostSoldProductsResult> mostsold = await context.Products.Where(prd => prd.OrderItems.Count > 0)
           .OrderByDescending(prd => prd.OrderItems.Count()).Select(p => new GetMostSoldProductsResult
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
               CreatedOn = p.CreatedOn
           }).Take(20).ToListAsync(cancellationToken);

        return mostsold;
    }
}