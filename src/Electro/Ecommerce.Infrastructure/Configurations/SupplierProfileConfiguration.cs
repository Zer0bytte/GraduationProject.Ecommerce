
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Ecommerce.Infrastructure.Configurations;

internal class SupplierProfileConfiguration : IEntityTypeConfiguration<SupplierProfile>
{
    public void Configure(EntityTypeBuilder<SupplierProfile> builder)
    {
        builder.Property(a => a.Governorate).HasConversion(
            to => to.ToString(),
            db => (Governorate)Enum.Parse(typeof(Governorate), db));
    }
}
