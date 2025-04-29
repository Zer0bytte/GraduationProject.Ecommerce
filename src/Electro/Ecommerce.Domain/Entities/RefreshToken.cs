using Ecommerce.Domain.Shared;

namespace Ecommerce.Domain.Entities;
public class RefreshToken : BaseEntity
{
    public string Token { get; set; } = default!;
    public Guid UserId { get; set; }
    public AppUser User { get; set; }
    public DateTime ExpiresOnUtc { get; set; }
}