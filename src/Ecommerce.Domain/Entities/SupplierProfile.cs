using Ecommerce.Domain.Shared;

namespace Ecommerce.Domain.Entities;

public class SupplierProfile : BaseEntity
{
    public Guid UserId { get; set; }
    public AppUser User { get; set; }
    public string BusinessName { get; set; } = default!;
    public string StoreName { get; set; } = default!;
    public decimal Balance { get; set; }
    public string TaxNumber { get; set; } = default!;
    public string NationalId { get; set; } = default!;
    public List<Product> Products { get; set; } = [];
    public List<SupplierBalanceTransaction> BalanceTransactions { get; set; } = [];
    public string NationalIdFrontNameOnServer { get; set; } = default!;
    public string NationalIdBackNameOnServer { get; set; } = default!;
    public string TaxCardNameOnServer { get; set; } = default!;
    public bool IsVerified { get; set; }
    public string? VerificationFailureReason { get; set; }
    public bool IsBanned { get; set; }

    public void Verify()
    {
        IsVerified = true;
        ModifiedOn = DateTime.UtcNow;
    }

    public bool IsSupplierVerified()
    {
        return IsVerified;
    }
}
