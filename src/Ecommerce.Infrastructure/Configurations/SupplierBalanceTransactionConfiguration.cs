
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations;



internal class SupplierBalanceTransactionConfiguration : IEntityTypeConfiguration<SupplierBalanceTransaction>
{
    public void Configure(EntityTypeBuilder<SupplierBalanceTransaction> builder)
    {
        builder.Property(t => t.TransactionType).HasConversion(type => type.ToString(),
            dbType => (TransactionType)Enum.Parse(typeof(TransactionType), dbType));
    }
}