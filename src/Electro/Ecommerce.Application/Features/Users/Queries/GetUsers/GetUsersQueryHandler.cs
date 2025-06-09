using Ecommerce.Application.Features.Categories.Queries.GetCategories;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ecommerce.Application.Features.Users.Queries.GetAdminUsers;

public class GetUsersQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetUsersQuery, PagedResult<GetUsersResult>>
{
    public async Task<PagedResult<GetUsersResult>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        IdentityRole<Guid>? role = await context.Roles
            .Where(r => r.Name == query.UsersType.ToString())
            .FirstOrDefaultAsync(cancellationToken);

        if (role == null)
            return null;

        var source = context.Users
              .Where(u => context.UserRoles
                  .Where(ur => ur.RoleId == role.Id)
                  .Select(ur => ur.UserId)
                  .Contains(u.Id));

        if (!string.IsNullOrWhiteSpace(query.SearchQuery))
        {
            source = source.Where(u => u.FullName.Contains(query.SearchQuery)
            || u.Email!.Contains(query.SearchQuery));
        }


        source = source.Skip((query.Page - 1) * query.Limit)
            .Take(query.Limit);

        var usersInRole = source
                .Select(user => new GetUsersResult
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    IsActive = !user.LockoutEnd.HasValue
                });


        var count = await source.CountAsync();
        return PagedResult<GetUsersResult>.Create(query, count, usersInRole); ;

    }
}
