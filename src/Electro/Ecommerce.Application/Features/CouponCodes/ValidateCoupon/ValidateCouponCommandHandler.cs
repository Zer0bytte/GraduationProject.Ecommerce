namespace Ecommerce.Application.Features.CouponCodes.ValidateCoupon;

public class ValidateCouponCommandHandler(IApplicationDbContext context) : IRequestHandler<ValidateCouponCommand, ValidateCouponResult>
{
    public async Task<ValidateCouponResult> Handle(ValidateCouponCommand command, CancellationToken cancellationToken)
    {
        CouponCode? couponCode = await context.CouponCodes.FirstOrDefaultAsync(c => c.Code == command.Code);
        if (couponCode is null) throw new NotFoundException("Coupon", command.Code);

        if (DateTime.Now > couponCode.ExpirationDate)
            throw new ExpiredCouponCodeException(command.Code);

        decimal discountValue = couponCode.DiscountPercentage / 100 * command.TotalPrice;
        if (discountValue > couponCode.MaximumDiscountValue)
            discountValue = couponCode.MaximumDiscountValue;

        decimal newPrice = command.TotalPrice - discountValue;

        return new ValidateCouponResult
        {
            NewDiscountedPrice = newPrice,
            DiscountValue = discountValue
        };
    }
}
