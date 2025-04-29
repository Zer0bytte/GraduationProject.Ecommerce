using Microsoft.EntityFrameworkCore;
using ShipX.Domain.Entities;

namespace ShipX.Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<Shipment> Shipments { get; set; }
    DbSet<ShipmentEvent> ShipmentEvents { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}