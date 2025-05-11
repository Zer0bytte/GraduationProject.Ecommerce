namespace Ecommerce.Application.Features.Users.Queries.GetAdminUsers;

public class GetUsersQueryHandler(UserManager<AppUser> userManager)
    : IRequestHandler<GetUsersQuery, IEnumerable<GetUsersResult>>
{
    public async Task<IEnumerable<GetUsersResult>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        IList<AppUser> users = await userManager.GetUsersInRoleAsync(request.UsersType.ToString());
        return users.Select(user => new GetUsersResult
        {
            Id = user.Id,
            Email = user.Email,
            FullName = user.FullName,
            IsActive = !user.LockoutEnd.HasValue
        }).ToList();

    }
}
