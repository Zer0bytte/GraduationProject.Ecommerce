using Ecommerce.Domain.Shared;

namespace Ecommerce.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = default!;
    public string ImageNameOnServer { get; set; } = default!;
}
