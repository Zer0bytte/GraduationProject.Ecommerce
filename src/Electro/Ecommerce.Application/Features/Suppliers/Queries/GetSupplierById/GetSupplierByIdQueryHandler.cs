namespace Ecommerce.Application.Features.Suppliers.Queries.GetSupplierById;

public class GetSupplierByIdQueryHandler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetSupplierByIdQuery, GetSupplierByIdResult>
{
    public async Task<GetSupplierByIdResult> Handle(GetSupplierByIdQuery query, CancellationToken cancellationToken)
    {

        SupplierProfile? supplier = await context.SupplierProfiles.FindAsync(query.Id);
        if (supplier is null) throw new NotFoundException("Supplier", query.Id);
        HttpRequest? httpRequest = httpContextAccessor.HttpContext?.Request;

        string imageUrl = httpRequest?.Scheme + "://" + httpRequest?.Host + "/media/";
        return new GetSupplierByIdResult
        {
            Id = supplier.Id,
            BusinessName = supplier.BusinessName,
            StoreName = supplier.StoreName,
            TaxNumber = supplier.TaxNumber,
            IsVerified = supplier.IsVerified,
            NationalIdNumber = supplier.NationalId,
            NationalIdFront = imageUrl + supplier.NationalIdFrontNameOnServer,
            NationalIdBack = imageUrl + supplier.NationalIdBackNameOnServer,
            TaxCard = imageUrl + supplier.TaxCardNameOnServer,

        };

    }
}
