namespace Ecommerce.Application.Features.Suppliers.Commands.VerifySupplier;

public class VerifySupplierCommandHandler(IApplicationDbContext context, IBus bus) : IRequestHandler<VerifySupplierCommand, VerifySupplierResult>
{
    public async Task<VerifySupplierResult> Handle(VerifySupplierCommand command, CancellationToken cancellationToken)
    {
        SupplierProfile? supplier = await context.SupplierProfiles.Include(s => s.User).FirstOrDefaultAsync(s => s.Id == command.SupplierId);

        if (supplier is null) throw new NotFoundException("Supplier", command.SupplierId);

        if (supplier.IsVerified()) throw new InternalServerException("Supplier Already Verified");

        supplier.Verify();

        await context.SaveChangesAsync(cancellationToken);

        await bus.Publish(new SupplierVerifiedEvent
        {
            SupplierName = supplier.User.FullName,
            StoreName = supplier.StoreName,
            BusinessName = supplier.BusinessName,
            Email = supplier.User.Email

        },
        cancellationToken);

        return new VerifySupplierResult
        {
            IsSuccess = true
        };

    }
}
