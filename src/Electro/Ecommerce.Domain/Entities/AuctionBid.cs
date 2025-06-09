using Ecommerce.Domain.Shared;

namespace Ecommerce.Domain.Entities;
public class AuctionBid : BaseEntity
{
    public decimal Price { get; set; }
    public AppUser User { get; set; } = default!;
    public Guid UserId { get; set; }

}