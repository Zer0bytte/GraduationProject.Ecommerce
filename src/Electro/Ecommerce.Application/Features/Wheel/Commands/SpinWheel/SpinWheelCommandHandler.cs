using StackExchange.Redis;
namespace Ecommerce.Application.Features.Wheel.Commands.SpinWheel;

public class SpinWheelCommandHandler(
    IApplicationDbContext context,
    ICurrentUser currentUser,
    IConnectionMultiplexer redis)
    : IRequestHandler<SpinWheelCommand, SpinWheelResult>
{
    private readonly IDatabase _cache = redis.GetDatabase();

    public async Task<SpinWheelResult> Handle(SpinWheelCommand request, CancellationToken cancellationToken)
    {
        Guid userId = currentUser.Id;
        string key = GetRedisKey(userId);

        int currentSpins = await GetCurrentSpinCount(key);

        if (currentSpins >= 3)
        {

            //throw new Exceptions.ApplicationException("لقد استهلكت محاولاتك اليومية. حاول مرة أخرى غداً.");
        }

        List<WheelReward> rewards = await context.WheelRewards.ToListAsync(cancellationToken);

        double totalProbability = rewards.Sum(r => r.Probability);
        if (Math.Abs(totalProbability - 1.0) > 0.0001)
        {
            throw new Exceptions.ApplicationException("مجموع احتمالات الجوائز غير صحيح. يرجى التواصل مع الإدارة.");
        }

        WheelReward selectedReward = GetRandomReward(rewards);

        await RegisterSpin(key, currentSpins);

        if (selectedReward.IsExtraChance && selectedReward.ExtraRetries.HasValue && selectedReward.ExtraRetries.Value > 0)
        {
            await _cache.StringDecrementAsync(key, selectedReward.ExtraRetries.Value);
        }

        return new SpinWheelResult
        {
            Success = true,
            Reward = selectedReward.Name,
            Value = selectedReward.Value
        };
    }

    private WheelReward GetRandomReward(List<WheelReward> rewards)
    {
        Random rnd = new Random();
        double roll = rnd.NextDouble();
        double cumulative = 0.0;

        foreach (WheelReward? reward in rewards.OrderBy(r => r.Probability))
        {
            cumulative += reward.Probability;
            if (roll < cumulative)
                return reward;
        }

        return rewards.Last();
    }

    private string GetRedisKey(Guid userId)
    {
        string today = DateTime.UtcNow.ToString("yyyy-MM-dd");
        return $"spin:user:{userId}:{today}";
    }

    private async Task<int> GetCurrentSpinCount(string key)
    {
        RedisValue count = await _cache.StringGetAsync(key);
        if (count.IsNullOrEmpty) return 0;
        return (int)count;
    }

    private async Task RegisterSpin(string key, int current)
    {
        if (current == 0)
        {
            await _cache.StringSetAsync(key, 1, TimeSpan.FromDays(1));
        }
        else
        {
            await _cache.StringSetAsync(key, current + 1, TimeSpan.FromDays(1));
        }
    }
}
