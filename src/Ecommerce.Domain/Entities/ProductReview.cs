using Ecommerce.Domain.Shared;

namespace Ecommerce.Domain.Entities;

public class ProductReview : BaseEntity
{
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public AppUser User { get; set; }
    public int Stars { get; set; }
    public string ReviewText { get; set; } = default!;
    public string? ReviewImageNameOnServer { get; set; }
}
