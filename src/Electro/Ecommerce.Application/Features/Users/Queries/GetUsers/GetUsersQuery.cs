using Ecommerce.Application.Common.Enums;

namespace Ecommerce.Application.Features.Users.Queries.GetUsers;

public class GetUsersQuery : PagedQuery, IRequest<PagedResult<GetUsersResult>>
{
    public string? SearchQuery { get; set; }
    public UserTypes UsersType { get; set; }
}
