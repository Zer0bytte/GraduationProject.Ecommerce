namespace Ecommerce.Application.Features.CouponCodes.Commands.DeactivateCouponCode;
public class DeactivateCouponCodeCommandHandler(IApplicationDbContext context) : IRequestHandler<DeactivateCouponCodeCommand, DeactivateCouponCodeResult>
{
    public async Task<DeactivateCouponCodeResult> Handle(DeactivateCouponCodeCommand command, CancellationToken cancellationToken)
    {
        CouponCode? code = await context.CouponCodes.FindAsync(command.Id);
        if (code is null) throw new NotFoundException("لم يتم العثور علي هذا الكود");

        code.Deactivate();

        await context.SaveChangesAsync(cancellationToken);

        return new DeactivateCouponCodeResult
        {
            IsSuccess = true
        };
    }
}