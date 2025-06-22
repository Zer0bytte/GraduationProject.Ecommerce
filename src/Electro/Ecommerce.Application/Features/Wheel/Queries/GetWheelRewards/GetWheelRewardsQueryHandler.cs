namespace Ecommerce.Application.Features.Wheel.Queries.GetWheelRewards;
public class GetWheelRewardsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetWheelRewardsQuery, GetWheelRewardsResult>
{
    public async Task<GetWheelRewardsResult> Handle(GetWheelRewardsQuery request, CancellationToken cancellationToken)
    {
        List<string> wheelOptions = await context.WheelRewards.Select(wr => wr.Name).ToListAsync(cancellationToken);

        return new GetWheelRewardsResult
        {
            Rewards = wheelOptions
        };
    }
}
