using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Ecommerce.Application.Features.Charts.GetGlobalCharts;
public class GetGlobalChartsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetGlobalChartsQuery, GetGlobalChartsResult>
{
    public async Task<GetGlobalChartsResult> Handle(GetGlobalChartsQuery request, CancellationToken cancellationToken)
    {
        List<UsersEachMonthResult> usersEachMonth = await context.Users.Where(u => !u.SupplierProfileId.HasValue)
            .OrderByDescending(u => u.CreatedOn)
             .GroupBy(u => new { u.CreatedOn.Year, u.CreatedOn.Month })
             .Select(g => new UsersEachMonthResult
             {
                 Date = g.Key.Year.ToString() + "-" + g.Key.Month.ToString("D2"),
                 RegisteredUsersCount = g.Count()
             })
             .ToListAsync(cancellationToken);

        var role = await context.Roles
            .FirstOrDefaultAsync(r => r.Name == "Admin", cancellationToken);

        var adminUsersCount = await context.Users
            .Join(context.UserRoles,
                user => user.Id,
                userRole => userRole.UserId,
                (user, userRole) => new { user, userRole })
            .Where(x => x.userRole.RoleId == role.Id)
            .Select(x => x.user)
            .CountAsync(cancellationToken);


        int suppliersCount = await context.Users.Where(u => u.SupplierProfileId.HasValue).CountAsync(cancellationToken);
        int ordersCount = await context.Orders.CountAsync(cancellationToken);
        int productsCount = await context.Products.CountAsync(cancellationToken);
        return new()
        {
            UsersReport = usersEachMonth,
            TotalUsersCount = usersEachMonth.Sum(u => u.RegisteredUsersCount),
            TotalSuppliersCount = suppliersCount,
            OrdersCount = ordersCount,
            ProductsCount = productsCount,
            AdminUsersCount = adminUsersCount
        };
    }
}
