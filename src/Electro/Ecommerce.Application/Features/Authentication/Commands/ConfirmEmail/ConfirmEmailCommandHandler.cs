namespace Ecommerce.Application.Features.Authentication.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler(UserManager<AppUser> userManager) : IRequestHandler<ConfirmEmailCommand, ConfirmEmailResult>
{
    public async Task<ConfirmEmailResult> Handle(ConfirmEmailCommand command, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByIdAsync(command.UserId.ToString());
        if (user is null)
            throw new NotFoundException("User", command.UserId);

        if (user.EmailConfirmed)
            throw new InternalServerException("User email is already confirmed");

        IdentityResult confiramtionResult = await userManager.ConfirmEmailAsync(user, command.Code.ToString());
        if (confiramtionResult.Succeeded)
        {
            return new ConfirmEmailResult
            {
                IsSuccess = true
            };
        }
        else
        {
            throw new InternalServerException("Invalid confirmation code!");
        }
    }
}
