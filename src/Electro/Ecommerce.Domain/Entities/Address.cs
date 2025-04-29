using Ecommerce.Domain.Enums;
using Ecommerce.Domain.Shared;

namespace Ecommerce.Domain.Entities;

public class Address : BaseEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public Governorate Governorate { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public AppUser User { get; set; }
    public Guid UserId { get; set; }
}


