using Ecommerce.Domain.Enums;
using Ecommerce.Domain.Shared;

namespace Ecommerce.Domain.Entities;

public enum VerificationStatus
{
    Pending,
    Verified,
    Rejected,
    Banned
}
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
    public byte[]? NationalIdFrontNameOnServer { get; set; } 
    public byte[]? NationalIdBackNameOnServer { get; set; } 
    public byte[]? TaxCardNameOnServer{ get; set; }

 
    public string? VerificationFailureReason { get; set; }
    public Governorate Governorate { get; set; }
    public VerificationStatus VerificationStatus { get; set; } = VerificationStatus.Pending;


    public void Verify()
    {
        VerificationStatus = VerificationStatus.Verified;
        ModifiedOn = DateTime.UtcNow;
    }

    public void RejectSupplier(string reason)
    {
        VerificationFailureReason = reason;
        VerificationStatus = VerificationStatus.Rejected;
    }

    public void BanSupplier()
    {
        VerificationStatus = VerificationStatus.Banned;
    }
    public bool IsVerified()
    {
        return VerificationStatus == VerificationStatus.Verified;
    }
}
