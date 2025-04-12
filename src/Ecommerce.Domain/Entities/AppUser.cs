using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Domain.Entities;

public class AppUser : IdentityUser<Guid>
{
    public string FullName { get; set; } = default!;
    public bool IsSeller { get; set; }
    public Guid? SupplierProfileId { get; set; }
    public SupplierProfile? SupplierProfile { get; set; }
    public ICollection<Address> Addresses { get; set; } = [];
}
