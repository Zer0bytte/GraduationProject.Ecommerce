namespace Ecommerce.Application.Features.Suppliers.Queries.GetAllSuppliers;

public record GetAllSuppliersResult
{
    public Guid SupplierId { get; set; }
    public string FullName { get; set; } = default!;
    public string StoreName { get; set; } = default!;
    public string BusinessName { get; set; } = default!;
    public bool IsVerified { get; set; }
}
