using Ecommerce.Domain.Exceptions;
using Ecommerce.Domain.Shared;

namespace Ecommerce.Domain.Entities;

public class CouponCode : BaseEntity
{
    public string Code { get; set; } = default!;
    public string? Description { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal MaximumDiscountValue { get; set; }
    public DateTime ExpirationDate { get; set; }
    public bool IsActive { get; set; } = true;


    public void Deactivate()
    {
        if (!IsActive) throw new DomainException("هذا الكود غير نشط بالفعل");
        IsActive = false;
    }

    public void Reactivate()
    {
        if (IsActive) throw new DomainException("هذا الكود نشط بالفعل");
        IsActive = true;
    }
}
