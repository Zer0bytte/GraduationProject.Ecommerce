using Ecommerce.Domain.Shared;

namespace Ecommerce.Domain.Entities;
public class WheelReward : BaseEntity
{
    public string Name { get; set; } = default!;
    public string? Value { get; set; }
    public double Probability { get; set; }
}