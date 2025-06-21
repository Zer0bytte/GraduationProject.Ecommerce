using System.Buffers.Text;

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
            VerificationStatus = supplier.VerificationStatus,
            NationalIdNumber = supplier.NationalId,
            NationalIdFront = $"data:image/jpeg;base64,{Convert.ToBase64String(supplier.NationalIdFrontNameOnServer)}",
            NationalIdBack = $"data:image/jpeg;base64,{Convert.ToBase64String(supplier.NationalIdBackNameOnServer)}",
            TaxCard = $"data:image/jpeg;base64,{Convert.ToBase64String(supplier.TaxCardNameOnServer)}",

        };

    }
}
