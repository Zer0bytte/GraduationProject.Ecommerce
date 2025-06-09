using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Ecommerce.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ProductReview> ProductReviews { get; set; }
    public DbSet<TransactionRefernce> TransactionRefernces { get; set; }
    public DbSet<CouponCode> CouponCodes { get; set; }
    public DbSet<SupplierProfile> SupplierProfiles { get; set; }
    public DbSet<ProductOption> ProductOptions { get; set; }
    public DbSet<SupplierBalanceTransaction> SupplierBalanceTransactions { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<AuctionBid> AuctionBids { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }
    private static void SetGlobalQueryFilter<TEntity>(ModelBuilder modelBuilder) where TEntity : BaseEntity
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        builder.Entity<SupplierProfile>()
          .HasOne(s => s.User)
          .WithOne(u => u.SupplierProfile)
          .HasForeignKey<SupplierProfile>(s => s.UserId);

        builder.Entity<OrderItem>().HasOne(oi => oi.Order)
           .WithMany(o => o.OrderItems)
           .HasForeignKey(oi => oi.OrderId);


        builder.Entity<OrderItem>().HasIndex(p => p.ProductId);
        builder.Entity<Order>().HasIndex(p => p.UserId);

        builder.Entity<Product>().HasOne(p => p.Supplier)
                  .WithMany(s => s.Products)
                  .HasForeignKey(p => p.SupplierId)
                  .OnDelete(DeleteBehavior.Restrict);

        foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType entityType in builder.Model.GetEntityTypes())
        {
            if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                MethodInfo? method = typeof(ApplicationDbContext)
                    .GetMethod(nameof(SetGlobalQueryFilter), BindingFlags.NonPublic | BindingFlags.Static)
                    ?.MakeGenericMethod(entityType.ClrType);

                method?.Invoke(null, [builder]);
            }
        }
    }
}
