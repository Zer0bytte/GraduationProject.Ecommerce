namespace Ecommerce.Application.Features.Products.Queries.GetProductByd;

public class GetProductByIdQueryHandler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ICurrentUser currentUser) : IRequestHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        HttpRequest? httpRequest = httpContextAccessor.HttpContext?.Request;
        string imageUrl = httpRequest?.Scheme + "://" + httpRequest?.Host + "/media/";
        GetProductByIdResult? product = await context.Products.Select(product => new GetProductByIdResult
        {
            Id = product.Id,
            Title = product.Title,
            Category = product.Category.Name,
            Description = product.Description,
            Images = product.Images.Select(p => new ImageResult { Url = imageUrl + p.NameOnServer, Id = p.Id }).ToArray(),
            Brand = product.Brand,
            Price = product.Price,
            DiscountedPrice = product.Discount >= 1 ? product.Price * (1 - product.Discount / 100m) : 0,
            DiscountPercentage = product.Discount,
            Stock = product.Stock,
            StockStatus = CalculateStockStatus(product.Stock),
            Tags = product.Tags,
            SKU = product.SKU,
            Rate = product.Reviews.Count > 0 ? product.Reviews.Sum(r => r.Stars) / product.Reviews.Count : 0,
            SupplierId = product.Supplier.UserId,
            StoreName = product.Supplier.StoreName,

            ProductOptions = product.Options.GroupBy(
                po => po.OptionGroupName).Select(g => new OptionGroupDto
                {
                    OptionGroupName = g.Key,
                    Options = g.Select(opt => new OptionDto
                    {
                        OptionName = opt.OptionName,
                        OptionPrice = opt.OptionPrice
                    }).ToList()
                }).ToList(),
            CanReview = context.OrderItems.Any(oi => oi.ProductId == product.Id && currentUser.IsAuthenticated && oi.Order.UserId == currentUser.Id
            && !product.Reviews.Any(r => r.UserId == currentUser.Id)),
            ProductReviews = product.Reviews.Select(rev => new ProductReviewResult
            {
                FullName = rev.User.FullName,
                ReviewText = rev.ReviewText,
                Stars = rev.Stars,
                ReviewImage = imageUrl + rev.ReviewImageNameOnServer
            }).ToList()

        }).FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken: cancellationToken);

        if (product is null)
            throw new NotFoundException("Product", query.Id);




        return product;


    }

    private static StockStatus CalculateStockStatus(int stock)
    {
        if (stock > 10) return StockStatus.InStock;

        if (stock < 10 && stock > 0) return StockStatus.LowStock;

        if (stock <= 0) return StockStatus.OutOfStock;

        return StockStatus.InStock;
    }
}
