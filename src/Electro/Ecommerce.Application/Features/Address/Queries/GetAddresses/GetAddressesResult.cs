namespace Ecommerce.Application.Features.Address.Queries.GetAddresses;

public record GetAddressesResult
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public Governorate Governorate { get; set; }
}
