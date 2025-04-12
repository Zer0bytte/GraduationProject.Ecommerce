namespace Ecommerce.Application.Features.Users.Queries.GetAdminUsers;

public class GetAdminUsersQueryHandler(UserManager<AppUser> userManager)
    : IRequestHandler<GetAdminUsersQuery, IEnumerable<GetAdminUsersResult>>
{
    public async Task<IEnumerable<GetAdminUsersResult>> Handle(GetAdminUsersQuery request, CancellationToken cancellationToken)
    {
        IList<AppUser> users = await userManager.GetUsersInRoleAsync("Admin");
        return users.Select(user => new GetAdminUsersResult
        {
            Id = user.Id,
            Email = user.Email,
            FullName = user.FullName,

        }).ToList();

    }
}
