namespace Ecommerce.Application.Features.CouponCodes.Commands.AddCouponCode;

public class AddCouponCodeCommand : IRequest<AddCouponCodeResult>
{
    public string Code { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal DiscountPercentage { get; set; }
    public decimal MaximumDiscountValue { get; set; }
    public DateTime ExpirationDate { get; set; }
}
