using Ecommerce.Domain.Shared;

namespace Ecommerce.Domain.Entities;

public class ProductImage : BaseEntity
{
    public string NameOnServer { get; set; } = default!;
    public Guid ProductId { get; set; }
}