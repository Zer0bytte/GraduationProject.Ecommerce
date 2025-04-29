namespace Ecommerce.Application.Features.Categories.Queries.GetCategories;

public class GetCategoriesQueryHandler(IApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : IRequestHandler<GetCategoriesQuery, PagedResult<GetCategoriesResult>>
{
    public async Task<PagedResult<GetCategoriesResult>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        DbSet<Category> source = context.Categories;

        HttpRequest? httpRequest = httpContextAccessor.HttpContext?.Request;
        string imageUrl = httpRequest?.Scheme + "://" + httpRequest?.Host + "/media/";

        IQueryable<GetCategoriesResult> categories = source.Select(c => new GetCategoriesResult
        {
            Id = c.Id,
            Name = c.Name,
            Image = imageUrl + c.ImageNameOnServer
        })
            .Skip((query.Page - 1) * query.Limit)
            .Take(query.Limit);


        int total = await source.CountAsync();

        return PagedResult<GetCategoriesResult>.Create(query, total, categories);
    }
}
