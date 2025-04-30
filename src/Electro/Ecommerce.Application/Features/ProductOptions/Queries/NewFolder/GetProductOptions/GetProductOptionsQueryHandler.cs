
using Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.Features.ProductOptions.Queries.NewFolder.GetProductOptions;
public class GetProductOptionsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetProductOptionsQuery, List<GetProductOptionsResult>>
{
    public async Task<List<GetProductOptionsResult>> Handle(GetProductOptionsQuery request, CancellationToken cancellationToken)
    {
        var groupedOptions = await context.ProductOptions
        .GroupBy(po => po.OptionGroupName)
        .Select(g => new GetProductOptionsResult
        {
            OptionGroup = g.Key,
            Values = g.Select(o=> o.OptionName).ToList()
        })
        .ToListAsync(cancellationToken);

        return groupedOptions;

    }
}