namespace Ecommerce.Application.Features.Identity.Authentication.Queries.GetInfo;

public class GetInfoQueryHandler(UserManager<AppUser> userManager, ICurrentUser ICurrentUser) : IRequestHandler<GetInfoQuery, GetInfoResult>
{
    public async Task<GetInfoResult> Handle(GetInfoQuery request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByIdAsync(ICurrentUser.Id.ToString());
        if (user is null) throw new UserNotFoundException(ICurrentUser.Id);

        return new GetInfoResult
        {
            FullName = user.FullName,
            Email = user.Email!,
            PhoneNumber = user.PhoneNumber
        };
    }
}
