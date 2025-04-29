namespace Ecommerce.Application.Features.Authentication.Queries.GetInfo;

public class GetInfoQueryHandler(UserManager<AppUser> userManager, ICurrentUser currentUser) : IRequestHandler<GetInfoQuery, GetInfoResult>
{
    public async Task<GetInfoResult> Handle(GetInfoQuery request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByIdAsync(currentUser.Id.ToString());
        if (user is null) throw new UserNotFoundException(currentUser.Id);

        return new GetInfoResult
        {
            FullName = user.FullName,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber,
            Role = currentUser.UserType
        };
    }
}
