namespace Ecommerce.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Address> Addresses { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<CouponCode> CouponCodes { get; set; }
    DbSet<OrderItem> OrderItems { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<ProductImage> ProductImages { get; set; }
    DbSet<ProductOption> ProductOptions { get; set; }
    DbSet<ProductReview> ProductReviews { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<SupplierProfile> SupplierProfiles { get; set; }
    DbSet<TransactionRefernce> TransactionRefernces { get; set; }
    DbSet<SupplierBalanceTransaction> SupplierBalanceTransactions { get; set; }
    DbSet<Conversation> Conversations { get; set; }
    DbSet<Message> Messages { get; set; }
    DbSet<AuctionBid> AuctionBids { get; set; }
    DbSet<RefreshToken> RefreshTokens { get; set; }
    DbSet<AppUser> Users { get; }
    DbSet<IdentityRole<Guid>> Roles { get; }
    DbSet<IdentityUserRole<Guid>> UserRoles { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}