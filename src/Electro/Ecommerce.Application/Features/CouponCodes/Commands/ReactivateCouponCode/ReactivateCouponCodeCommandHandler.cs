namespace Ecommerce.Application.Features.CouponCodes.Commands.ReactivateCouponCode;
public class ReactivateCouponCodeCommandHandler(IApplicationDbContext context) : IRequestHandler<ReactivateCouponCodeCommand, ReactivateCouponCodeResult>
{
    public async Task<ReactivateCouponCodeResult> Handle(ReactivateCouponCodeCommand command, CancellationToken cancellationToken)
    {
        CouponCode? code = await context.CouponCodes.FindAsync(command.Id);
        if (code is null) throw new NotFoundException("لم يتم العثور علي هذا الكود");

        code.Reactivate();

        await context.SaveChangesAsync(cancellationToken);

        return new ReactivateCouponCodeResult
        {
            IsSuccess = true
        };
    }
}