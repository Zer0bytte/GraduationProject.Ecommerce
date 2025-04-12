using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.Common.Persistance;
public class PagedQuery
{
    [DefaultValue(PagedQueryConstants.PAGE_DEFAULT)]
    [Required]
    public int Page { get; set; } = PagedQueryConstants.PAGE_DEFAULT;

    private int _limit = PagedQueryConstants.LIMIT_DEFAULT;

    [DefaultValue(PagedQueryConstants.LIMIT_DEFAULT)]
    [Required]
    public int Limit
    {
        get => _limit;
        set => _limit = value > PagedQueryConstants.MAX_LIMIT ? PagedQueryConstants.MAX_LIMIT : value;
    }
}

public struct PagedQueryConstants
{
    public const int PAGE_DEFAULT = 1;
    public const int LIMIT_DEFAULT = 10;
    public const int MAX_LIMIT = 100;
}