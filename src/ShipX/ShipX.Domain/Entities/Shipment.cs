using ShipX.Domain.Emums;
using ShipX.Domain.Entities.Shared;
using ShipX.Domain.ValueObjects;

namespace ShipX.Domain.Entities;
public class Shipment : BaseEntity
{
    public string TrackingId { get; set; } = default!;
    public ShipmentService Service { get; set; }
    public decimal Cost { get; set; }
    public decimal ShippingCost { get; set; }
    public Address ShipFrom { get; set; } = default!;
    public Address ShipTo { get; set; } = default!;
    public decimal Weight { get; set; }
    public bool Fragile { get; set; }
    public AppUser User { get; set; }
    public Guid UserId { get; set; }
    public ICollection<ShipmentEvent> EventHistory { get; set; } = [];
}
