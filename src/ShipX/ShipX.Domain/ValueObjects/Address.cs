using ShipX.Domain.Entities;

namespace ShipX.Domain.ValueObjects;

public class Address
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public Governorate Governorate { get; set; }
    public string PhoneNumber { get; set; } = default!;
}
