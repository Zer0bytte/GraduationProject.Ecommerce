using ShipX.Domain.Emums;
using ShipX.Domain.Entities.Shared;

namespace ShipX.Domain.Entities;

public class ShipmentEvent : BaseEntity
{
    public Shipment Shipment { get; set; } = default!;
    public Guid ShipmentId { get; set; }
    public ShipmentEventType Type { get; set; }
    public Governorate Location { get; set; }
    public string? Note { get; set; }
}