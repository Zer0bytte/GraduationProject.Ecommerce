using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.ProductOptions.Queries.NewFolder.GetProductOptions;
public class GetProductOptionsResult
{
    public string OptionGroup { get; set; } = default!;
    public List<string> Values { get; set; } = [];
}

public record GroupResponse
{
}