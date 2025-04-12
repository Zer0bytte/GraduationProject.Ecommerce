using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations;

public class TransactionReferenceConfiguration : IEntityTypeConfiguration<TransactionRefernce>
{
    public void Configure(EntityTypeBuilder<TransactionRefernce> builder)
    {
        builder.Property(s => s.Status)
          .HasConversion(
              o => o.ToString(),
              o => (PaymentStatus)Enum.Parse(typeof(PaymentStatus), o)
          );
    }
}
