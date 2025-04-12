namespace Ecommerce.Application.Features.Users.Commands.SetPassword;

public class SetPasswordCommandHandler(UserManager<AppUser> userManager) : IRequestHandler<SetPasswordCommand, SetPasswordResult>
{
    public async Task<SetPasswordResult> Handle(SetPasswordCommand command, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByIdAsync(command.UserId.ToString());
        if (user is null) throw new NotFoundException("User", command.UserId);


        IdentityResult setResult = await userManager.ResetPasswordAsync(user, command.Token, command.Password);

        if (setResult.Succeeded)
        {
            string confirmToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
            await userManager.ConfirmEmailAsync(user, confirmToken);
            return new SetPasswordResult
            {
                IsSuccess = true
            };
        }


        List<string> errors = setResult.Errors.Select(e => e.Description).ToList();

        return new SetPasswordResult
        {
            IsSuccess = false,
            Errors = errors
        };

    }
}
