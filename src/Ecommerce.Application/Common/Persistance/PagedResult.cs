
namespace Ecommerce.Application.Common.Persistance;
public class PagedResult<TModel>
{
    public int Page { get; set; }
    public int Limit { get; set; }
    public int TotalItems { get; set; }
    public IList<TModel> Items { get; set; } = new List<TModel>();

    public static PagedResult<TModel> Create(int page, int limit, int totalItems, int totalPages, IEnumerable<TModel> items)
    {
        PagedResult<TModel> result = new PagedResult<TModel>
        {
            Page = page,
            Limit = limit,
            TotalItems = totalItems,
            Items = items.ToList()
        };
        return result;
    }

    public static PagedResult<TModel> Create(PagedQuery query, int totalItems, IEnumerable<TModel> items)
    {
        PagedResult<TModel> result = new PagedResult<TModel>
        {
            Page = query.Page,
            Limit = query.Limit,
            TotalItems = totalItems,
            Items = items.ToList()
        };
        return result;
    }

    public static PagedResult<TModel> CreateStatic(PagedQuery query, int totalItems, IList<TModel> items)
    {
        return new PagedResult<TModel>
        {
            Items = items,
            Limit = query.Limit,
            Page = query.Page,
            TotalItems = totalItems
        };
    }

}