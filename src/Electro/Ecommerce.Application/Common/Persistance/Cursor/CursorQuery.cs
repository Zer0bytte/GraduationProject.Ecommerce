using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Common.Persistance.Cursor;
public class CursorQuery
{
    public string? Cursor { get; set; }

    private int _limit = PagedQueryConstants.LIMIT_DEFAULT;

    [DefaultValue(PagedQueryConstants.LIMIT_DEFAULT)]
    [Required]
    public int Limit
    {
        get => _limit;
        set => _limit = value > PagedQueryConstants.MAX_LIMIT ? PagedQueryConstants.MAX_LIMIT : value;
    }
}