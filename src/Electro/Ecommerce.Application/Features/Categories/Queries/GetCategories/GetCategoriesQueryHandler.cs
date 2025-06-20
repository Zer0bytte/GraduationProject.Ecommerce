using Ecommerce.Application.Common.Configs;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.Features.Categories.Queries.GetCategories;

public class GetCategoriesQueryHandler(IApplicationDbContext context, HostingConfig hostingConfig, IDistributedCache cache) : IRequestHandler<GetCategoriesQuery, PagedResult<GetCategoriesResult>>
{
    public async Task<PagedResult<GetCategoriesResult>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        string cacheKey = $"categories:page={query.Page}:limit={query.Limit}";
        string cached = await cache.GetStringAsync(cacheKey);

        if (!string.IsNullOrEmpty(cached))
        {
            return JsonConvert.DeserializeObject<PagedResult<GetCategoriesResult>>(cached);
        }

        DbSet<Category> source = context.Categories;
        string imageUrl = hostingConfig.HostName + "/media/";
        
        IQueryable<GetCategoriesResult> categoriesQuery = source
            .Select(c => new GetCategoriesResult
            {
                Id = c.Id,
                Name = c.Name,
                Image = imageUrl + c.ImageNameOnServer
            })
            .Skip((query.Page - 1) * query.Limit)
            .Take(query.Limit);

        List<GetCategoriesResult> categoriesList = await categoriesQuery.ToListAsync();
        int total = await source.CountAsync();

        var result = PagedResult<GetCategoriesResult>.Create(query, total, categoriesList.AsQueryable());

        await cache.SetStringAsync(
            cacheKey,
            JsonConvert.SerializeObject(result),new DistributedCacheEntryOptions
            {
                
            });

        return result;
    }
}
