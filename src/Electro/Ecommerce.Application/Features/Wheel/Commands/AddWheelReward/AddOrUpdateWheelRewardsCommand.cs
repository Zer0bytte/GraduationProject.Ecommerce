namespace Ecommerce.Application.Features.Wheel.Commands.AddWheelReward;
public class AddOrUpdateWheelRewardsCommand : IRequest<AddOrUpdateWheelRewardsResult>
{
    public List<WheelRewardsDto> WheelRewards { get; set; } = [];
}


public record WheelRewardsDto
{
    public string Name { get; set; } = default!;
    public string? Value { get; set; }
    public double Probability { get; set; }
    public bool IsExtraChance { get; set; }
}