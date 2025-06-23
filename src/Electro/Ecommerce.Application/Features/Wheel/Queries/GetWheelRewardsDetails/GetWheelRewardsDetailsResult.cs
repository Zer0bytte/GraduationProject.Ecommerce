namespace Ecommerce.Application.Features.Wheel.Queries.GetWheelRewardsDetails;
public class GetWheelRewardsDetailsResult
{
    public string Name { get; set; } = default!;
    public string? Value { get; set; }
    public double Probability { get; set; }
    public bool IsExtraChance { get; set; }
    public int? ExtraRetries { get; set; }
}