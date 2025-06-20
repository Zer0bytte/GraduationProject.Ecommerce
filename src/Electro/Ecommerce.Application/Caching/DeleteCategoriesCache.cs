using StackExchange.Redis;

namespace Ecommerce.Application.Caching;

public class DeleteCategoriesCache(IConnectionMultiplexer connectionMultiplexer)
{
    private readonly IDatabase _redis = connectionMultiplexer.GetDatabase();

    public async Task DeleteAsync()
    {
        System.Net.EndPoint[] endpoints = connectionMultiplexer.GetEndPoints();
        IServer server = connectionMultiplexer.GetServer(endpoints.First());

        RedisKey[] keys = server.Keys(pattern: "graduation.ecommercecategories:*").ToArray();

        foreach (RedisKey key in keys)
        {
            await _redis.KeyDeleteAsync(key);
        }
    }
}
