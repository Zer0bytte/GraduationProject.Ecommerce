
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Configurations;

internal class DeleiveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.Property(e => e.Price).HasColumnType("decimal(18,2)");
    }
}
internal class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.Property(a => a.Governorate).HasConversion(
            to => to.ToString(),
            db => (Governorate)Enum.Parse(typeof(Governorate), db));
    }
}