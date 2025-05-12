using Google.Apis.Util;

namespace Ecommerce.Application.Features.CouponCodes.Commands.UpdateCouponCode;
public class UpdateCouponCodeCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateCouponCodeCommand, UpdateCouponCodeResult>
{
    public async Task<UpdateCouponCodeResult> Handle(UpdateCouponCodeCommand commad, CancellationToken cancellationToken)
    {
        CouponCode? code = await context.CouponCodes.FindAsync(commad.Id);
        if (code is null) throw new NotFoundException("لم يتم العثور علي هذا الكود");

        code.Code = commad.Code;
        code.ExpirationDate = commad.ExpirationDate;
        code.Description = commad.Description;
        code.MaximumDiscountValue = commad.MaximumDiscountValue;
        code.DiscountPercentage = commad.DiscountPercentage;
        code.ModifiedOn = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);
        return new UpdateCouponCodeResult
        {
            IsSuccess = true
        };
    }
}