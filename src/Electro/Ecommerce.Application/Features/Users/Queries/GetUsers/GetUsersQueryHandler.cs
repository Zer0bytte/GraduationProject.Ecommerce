using System.Data;

namespace Ecommerce.Application.Features.Users.Queries.GetAdminUsers;

public class GetUsersQueryHandler(UserManager<AppUser> userManager, IApplicationDbContext context)
    : IRequestHandler<GetUsersQuery, IEnumerable<GetUsersResult>>
{
    public async Task<IEnumerable<GetUsersResult>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        IdentityRole<Guid>? role = await context.Roles
            .Where(r => r.Name == request.UsersType.ToString())
            .FirstOrDefaultAsync(cancellationToken);

        if (role == null)
            return new List<GetUsersResult>();

        List<GetUsersResult> usersInRole = await context.Users
                .Where(u => context.UserRoles
                    .Where(ur => ur.RoleId == role.Id)
                    .Select(ur => ur.UserId)
                    .Contains(u.Id))
                .Select(user => new GetUsersResult
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    IsActive = !user.LockoutEnd.HasValue
                })
                .ToListAsync(cancellationToken);

        return usersInRole;

    }
}
