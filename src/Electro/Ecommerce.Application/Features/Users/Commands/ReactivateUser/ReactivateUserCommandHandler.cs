namespace Ecommerce.Application.Features.Users.Commands.ReactivateUser;
public class ReactivateUserCommandHandler(UserManager<AppUser> userManager) : IRequestHandler<ReactivateUserCommand, ReactivateUserResult>
{
    public async Task<ReactivateUserResult> Handle(ReactivateUserCommand command, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByIdAsync(command.UserId.ToString());
        if (user is null) throw new UserNotFoundException();

        if (!user.LockoutEnd.HasValue)
            throw new ApplicationException("هذا المستخدم نشط بالفعل");

        await userManager.SetLockoutEndDateAsync(user, null);

        return new ReactivateUserResult
        {
            IsSuccess = true
        };
    }
}