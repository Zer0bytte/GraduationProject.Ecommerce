namespace Ecommerce.Application.Features.Suppliers.Queries.GetCurrentSupplier;
public class GetCurrentSupplierQueryHandler(ICurrentUser currentUser, IApplicationDbContext context)
    : IRequestHandler<GetCurrentSupplierQuery, GetCurrentSupplierResult>
{
    public async Task<GetCurrentSupplierResult> Handle(GetCurrentSupplierQuery request, CancellationToken cancellationToken)
    {
        SupplierProfile? supplier = await context.SupplierProfiles.FindAsync(currentUser.SupplierId);
        if (supplier is null) throw new NotFoundException("Supplier");


        return new GetCurrentSupplierResult
        {
            Id = supplier.Id,
            BusinessName = supplier.BusinessName,
            StoreName = supplier.StoreName,
            VerificationStatus = supplier.VerificationStatus,
        };
    }
}