
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations;

internal class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.Property(a => a.Governorate).HasConversion(
            to => to.ToString(),
            db => (Governorate)Enum.Parse(typeof(Governorate), db));
    }
}