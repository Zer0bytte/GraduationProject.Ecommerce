namespace Ecommerce.Application.Features.Users.Commands.DeactivateUser;

public class DeactivateUserCommandHandler(UserManager<AppUser> userManager, ICurrentUser currentUser, IApplicationDbContext context) : IRequestHandler<DeactivateUserCommand, DeactivateUserResult>
{
    public async Task<DeactivateUserResult> Handle(DeactivateUserCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == currentUser.Id) throw new ApplicationException("لا يمكن تعطيل حسابك");

        AppUser? user = await userManager.FindByIdAsync(command.Id.ToString());
        if (user is null || user.LockoutEnd.HasValue) throw new UserNotFoundException();



        await userManager.SetLockoutEnabledAsync(user, true);

        await userManager.SetLockoutEndDateAsync(user, DateTime.Today.AddYears(10));

        context.RefreshTokens.RemoveRange(context.RefreshTokens.Where(r => r.UserId == user.Id).ToList());
        await context.SaveChangesAsync(cancellationToken);
        return new DeactivateUserResult
        {
            IsSuccess = true
        };

    }
}
