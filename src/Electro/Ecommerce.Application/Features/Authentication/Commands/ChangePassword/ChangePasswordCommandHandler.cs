namespace Ecommerce.Application.Features.Authentication.Commands.ChangePassword;
public class ChangePasswordCommandHandler(UserManager<AppUser> userManager, ICurrentUser currentUser, IApplicationDbContext context) : IRequestHandler<ChangePasswordCommand, ChangePasswordResult>
{
    public async Task<ChangePasswordResult> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByIdAsync(currentUser.Id.ToString());
        if (user is null) throw new UserNotFoundException();

        IdentityResult result = await userManager.ChangePasswordAsync(user, command.CurrentPassword, command.NewPassword);
        if (!result.Succeeded)
            throw new ApplicationException("حدث خطأ, برجاء المحاوله لاحقا");

        await context.RefreshTokens
            .Where(r => r.UserId == currentUser.Id)
            .ExecuteUpdateAsync(p => p
                .SetProperty(r => r.ExpiresOnUtc, r => DateTime.UtcNow.AddDays(-1)), cancellationToken);
        return new ChangePasswordResult { IsSuccess = true };
    }
}