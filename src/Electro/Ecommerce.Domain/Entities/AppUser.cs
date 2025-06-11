using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Domain.Entities;

public class AppUser : IdentityUser<Guid>
{
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public string FullName { get; set; } = default!;
    public bool IsSeller { get; set; }
    public Guid? SupplierProfileId { get; set; }
    public SupplierProfile? SupplierProfile { get; set; }
    public ICollection<Address> Addresses { get; set; } = [];
    public ICollection<Conversation> Conversations { get; set; } = [];

}
