namespace Ecommerce.Application.Features.CouponCodes;


public class AddCouponCodeCommandHandler(IApplicationDbContext context) : IRequestHandler<AddCouponCodeCommand, AddCouponCodeResult>
{
    public async Task<AddCouponCodeResult> Handle(AddCouponCodeCommand command, CancellationToken cancellationToken)
    {
        if (context.CouponCodes.Any(c => c.Code == command.Code))
            throw new CouponCodeAlreadyExistException(command.Code);


        CouponCode code = new CouponCode
        {
            Code = command.Code,
            Description = command.Description,
            ExpirationDate = command.ExpirationDate,
            DiscountPercentage = command.DiscountPercentage,
            MaximumDiscountValue = command.MaximumDiscountValue,
        };

        context.CouponCodes.Add(code);

        await context.SaveChangesAsync(cancellationToken);

        return new AddCouponCodeResult
        {
            IsSuccess = true
        };
    }
}
