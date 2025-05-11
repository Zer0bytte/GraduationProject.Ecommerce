using Ecommerce.Application.Common.Enums;

namespace Ecommerce.Application.Features.Users.Queries.GetAdminUsers;

public class GetUsersQuery : IRequest<IEnumerable<GetUsersResult>>
{
    public UserTypes UsersType { get; set; }
}
