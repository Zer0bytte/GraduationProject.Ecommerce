namespace Ecommerce.Application.Features.CouponCodes.Queries.GetCouponCodes;
public class GetCouponCodesQueryHandle(IApplicationDbContext context) : IRequestHandler<GetCouponCodesQuery, PagedResult<GetCouponCodesResult>>
{
    public async Task<PagedResult<GetCouponCodesResult>> Handle(GetCouponCodesQuery query, CancellationToken cancellationToken)
    {
        var source = context.CouponCodes.OrderByDescending(c => c.CreatedOn);

        List<GetCouponCodesResult> couponCodes = await source.Select(c => new GetCouponCodesResult
        {
            Id = c.Id,
            Code = c.Code,
            DiscountPercentage = c.DiscountPercentage,
            IsActive = c.IsActive,
            IsValid = DateTime.UtcNow < c.ExpirationDate
        })
            .Skip((query.Page - 1) * query.Limit)
            .Take(query.Limit).ToListAsync(cancellationToken);


        int count = await source.CountAsync();

        return PagedResult<GetCouponCodesResult>.Create(query, count, couponCodes);


    }
}