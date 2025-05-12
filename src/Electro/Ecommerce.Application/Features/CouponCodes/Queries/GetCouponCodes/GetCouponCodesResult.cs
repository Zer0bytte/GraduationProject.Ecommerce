namespace Ecommerce.Application.Features.CouponCodes.Queries.GetCouponCodes;
public class GetCouponCodesResult
{
    public Guid Id { get; set; }
    public string Code { get; set; } = default!;
    public decimal DiscountPercentage { get; set; }
    public bool IsActive { get; set; }
    public bool IsValid { get; set; }
}