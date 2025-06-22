namespace Ecommerce.Application.Features.Wheel.Commands.AddWheelReward;
public class AddOrUpdateWheelRewardsCommandHandler(IApplicationDbContext context) : IRequestHandler<AddOrUpdateWheelRewardsCommand, AddOrUpdateWheelRewardsResult>
{
    public async Task<AddOrUpdateWheelRewardsResult> Handle(AddOrUpdateWheelRewardsCommand request, CancellationToken cancellationToken)
    {
        await context.Database.ExecuteSqlRawAsync("DELETE FROM WheelRewards");
        IEnumerable<WheelReward> wheelRewards = request.WheelRewards.Select(wr => new WheelReward
        {
            Name = wr.Name,
            Probability = wr.Probability,
            Value = wr.Value,
            IsExtraChance = wr.IsExtraChance
        });
        context.WheelRewards.AddRange(wheelRewards);
        await context.SaveChangesAsync(cancellationToken);

        return new AddOrUpdateWheelRewardsResult
        {
            IsSuccess = true
        };
    }
}