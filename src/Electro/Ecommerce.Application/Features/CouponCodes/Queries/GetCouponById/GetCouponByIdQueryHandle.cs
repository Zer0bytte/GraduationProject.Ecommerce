using Ecommerce.Application.Features.CouponCodes.Queries.GetById;

namespace Ecommerce.Application.Features.CouponCodes.Queries.GetCouponById;
public class GetCouponByIdQueryHandle(IApplicationDbContext context) : IRequestHandler<GetCouponByIdQuery, GetCouponByIdResult>
{
    public async Task<GetCouponByIdResult> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
    {
        CouponCode? code = await context.CouponCodes.FindAsync(request.Id);
        if (code is null) throw new NotFoundException("لم يتم العثور علي هذا الكود");

        return new GetCouponByIdResult
        {
            Id = code.Id,
            Code = code.Code,
            Description = code.Description,
            DiscountPercentage = code.DiscountPercentage,
            ExpirationDate = code.ExpirationDate,
            IsActive = code.IsActive,
            MaximumDiscountValue = code.MaximumDiscountValue,
        };
    }
}