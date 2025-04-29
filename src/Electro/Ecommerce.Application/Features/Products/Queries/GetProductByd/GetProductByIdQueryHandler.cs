namespace Ecommerce.Application.Features.Products.Queries.GetProductByd;

public class GetProductByIdQueryHandler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor, ICurrentUser ICurrentUser) : IRequestHandler<GetProductByIdQuery, GetProductByIdResult>
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
            Images = product.Images.Select(p => imageUrl + p.NameOnServer).ToArray(),
            Brand = product.Brand,
            Price = product.Price,
            DiscountedPrice = product.Discount >= 1 ? product.Price * (1 - product.Discount / 100m) : 0,
            DiscountPercentage = product.Discount,
            Stocks = product.Stock,
            Tags = product.Tags,
            SKU = product.SKU,
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
                }).ToList()
        }).FirstOrDefaultAsync(x => x.Id == query.Id, cancellationToken: cancellationToken);

        if (product is null)
            throw new NotFoundException("Product", query.Id);


        if (ICurrentUser.Id != Guid.Empty)
            product.IsReviewd = context.ProductReviews.Any(rev => rev.ProductId == query.Id && rev.UserId == ICurrentUser.Id);

        product.ProductReviews = context.ProductReviews.Where(rev => rev.ProductId == query.Id).Select(rev => new ProductReviewResult
        {
            FullName = rev.User.FullName,
            ReviewText = rev.ReviewText,
            Stars = rev.Stars,
            ReviewImage = imageUrl + rev.ReviewImageNameOnServer
        }).ToList();
        return product;


    }
}
