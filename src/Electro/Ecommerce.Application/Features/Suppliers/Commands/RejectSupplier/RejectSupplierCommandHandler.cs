namespace Ecommerce.Application.Features.Suppliers.Commands.RejectSupplier;
public class RejectSupplierCommandHandler(IApplicationDbContext context) : IRequestHandler<RejectSupplierCommand, RejectSupplierResult>
{
    public async Task<RejectSupplierResult> Handle(RejectSupplierCommand command, CancellationToken cancellationToken)
    {
        SupplierProfile? supplier = await context.SupplierProfiles.FindAsync(command.SupplierId);

        if (supplier is null) throw new NotFoundException("Supplier", command.SupplierId);

        if (supplier.IsRejected) throw new InternalServerException("Supplier already rejectd!");

        supplier.RejectSupplier(command.RejectionResult);

        await context.SaveChangesAsync(cancellationToken);

        return new RejectSupplierResult
        {
            IsSuccess = true
        };
    }
}