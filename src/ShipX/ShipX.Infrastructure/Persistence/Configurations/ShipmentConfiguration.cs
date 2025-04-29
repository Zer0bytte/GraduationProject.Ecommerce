using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShipX.Domain.Emums;
using ShipX.Domain.Entities;
using System.Reflection.Emit;

namespace ShipX.Infrastructure.Persistence.Configurations;

internal class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
{
    public void Configure(EntityTypeBuilder<Shipment> builder)
    {
        builder.Property(s => s.Cost)
           .HasColumnType("decimal(18, 2)");  // Precision 18, Scale 2 (total digits and digits after decimal)

        builder.Property(s => s.ShippingCost)
            .HasColumnType("decimal(18, 2)");

        builder.Property(s => s.Weight)
            .HasColumnType("decimal(18, 2)");
        builder.OwnsOne(s => s.ShipFrom, a =>
        {
            a.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
            a.Property(p => p.LastName).IsRequired().HasMaxLength(100);
            a.Property(p => p.Street).IsRequired().HasMaxLength(200);
            a.Property(p => p.City).IsRequired().HasMaxLength(100);
            a.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(15);
            a.Property(p => p.Governorate).IsRequired();

            a.Property(p => p.Governorate).HasConversion(gov => gov.ToString(),
           dbGov => (Governorate)Enum.Parse(typeof(Governorate), dbGov));
        });

        builder.OwnsOne(s => s.ShipTo, a =>
        {
            a.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
            a.Property(p => p.LastName).IsRequired().HasMaxLength(100);
            a.Property(p => p.Street).IsRequired().HasMaxLength(200);
            a.Property(p => p.City).IsRequired().HasMaxLength(100);
            a.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(15);
            a.Property(p => p.Governorate).IsRequired();

            a.Property(p=>p.Governorate).HasConversion(gov => gov.ToString(),
           dbGov => (Governorate)Enum.Parse(typeof(Governorate), dbGov));
        });

        builder.Property(p => p.Service).HasConversion(serv => serv.ToString(),
                   dbServ => (ShipmentService)Enum.Parse(typeof(ShipmentService), dbServ));
    }
}


internal class ShipmentEventConfiguration : IEntityTypeConfiguration<ShipmentEvent>
{
    public void Configure(EntityTypeBuilder<ShipmentEvent> builder)
    {
        builder.Property(p => p.Type).HasConversion(typ => typ.ToString(),
            dbType => (ShipmentEventType)Enum.Parse(typeof(ShipmentEventType), dbType));

        builder.Property(p => p.Location).HasConversion(loc => loc.ToString(),
           dbLoc => (Governorate)Enum.Parse(typeof(Governorate), dbLoc));
    }
}