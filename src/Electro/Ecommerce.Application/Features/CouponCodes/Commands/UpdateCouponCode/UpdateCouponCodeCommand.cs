using Ecommerce.Application.Features.Conversations.Queries.GetConversations;

namespace Ecommerce.Application.Features.CouponCodes.Commands.UpdateCouponCode;
public class UpdateCouponCodeCommand : IRequest<UpdateCouponCodeResult>
{
    public Guid Id { get; set; }
    public string Code { get; set; } = default!;
    public string? Description { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal MaximumDiscountValue { get; set; }
    public DateTime ExpirationDate { get; set; }
}