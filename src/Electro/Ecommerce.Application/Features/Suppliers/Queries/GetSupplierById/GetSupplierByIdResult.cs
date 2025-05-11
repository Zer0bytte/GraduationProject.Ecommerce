namespace Ecommerce.Application.Features.Suppliers.Queries.GetSupplierById;

public record GetSupplierByIdResult
{
    public Guid Id { get; set; }
    public string BusinessName { get; set; } = default!;
    public string StoreName { get; set; } = default!;
    public string TaxNumber { get; set; } = default!;
    public string NationalIdNumber { get; set; } = default!;
    public string NationalIdFront { get; set; } = default!;
    public string NationalIdBack { get; set; } = default!;
    public string TaxCard { get; set; } = default!;
    public VerificationStatus VerificationStatus { get; set; }
}
