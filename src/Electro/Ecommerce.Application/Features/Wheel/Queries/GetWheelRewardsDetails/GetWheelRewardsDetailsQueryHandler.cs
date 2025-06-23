namespace Ecommerce.Application.Features.Wheel.Queries.GetWheelRewardsDetails;
public class GetWheelRewardsDetailsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetWheelRewardsDetailsQuery, List<GetWheelRewardsDetailsResult>>
{
    public async Task<List<GetWheelRewardsDetailsResult>> Handle(GetWheelRewardsDetailsQuery request, CancellationToken cancellationToken)
    {
        List<GetWheelRewardsDetailsResult> rewards = await context.WheelRewards.Select(wr => new GetWheelRewardsDetailsResult
        {
            Name = wr.Name,
            Value = wr.Value,
            ExtraRetries = wr.ExtraRetries,
            IsExtraChance = wr.IsExtraChance,
            Probability = wr.Probability,
        }).ToListAsync(cancellationToken);

        return rewards;
    }
}