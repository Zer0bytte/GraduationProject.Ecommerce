namespace Ecommerce.Application.Features.Suppliers.Commands.RejectSupplier;
public class RejectSupplierCommandHandler(IApplicationDbContext context) : IRequestHandler<RejectSupplierCommand, RejectSupplierResult>
{
    public async Task<RejectSupplierResult> Handle(RejectSupplierCommand command, CancellationToken cancellationToken)
    {
        SupplierProfile? supplier = await context.SupplierProfiles.FindAsync(command.SupplierId);

        if (supplier is null) throw new NotFoundException("Supplier", command.SupplierId);

        if (supplier.VerificationStatus == VerificationStatus.Rejected) throw new InternalServerException("هذا البائع مرفوض بالفعل");

        if (supplier.VerificationStatus == VerificationStatus.Verified) throw new InternalServerException("تم تأكيد هذا البائع بالفعل");

        supplier.RejectSupplier(command.RejectionResult);

        await context.SaveChangesAsync(cancellationToken);

        return new RejectSupplierResult
        {
            IsSuccess = true
        };
    }
}