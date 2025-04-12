using Ecommerce.Domain.Shared;

namespace Ecommerce.Domain.Entities;

public class ProductOption : BaseEntity
{
    public Product Product { get; set; } = default!;
    public Guid ProductId { get; set; }
    public string OptionGroupName { get; set; } = default!;
    public string OptionName { get; set; } = default!;
    public decimal OptionPrice { get; set; }
}
