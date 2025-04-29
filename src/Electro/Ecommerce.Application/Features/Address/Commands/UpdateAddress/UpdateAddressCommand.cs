

namespace Ecommerce.Application.Features.Address.Commands.UpdateAddress;

public class UpdateAddressCommand : IRequest<UpdateAddressResult>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public Governorate Governorate { get; set; }
}
