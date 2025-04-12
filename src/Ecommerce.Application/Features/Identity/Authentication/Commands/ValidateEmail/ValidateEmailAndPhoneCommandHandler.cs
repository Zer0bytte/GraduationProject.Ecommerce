namespace Ecommerce.Application.Features.Identity.Authentication.Commands.ValidateEmail;

public class ValidateEmailAndPhoneCommandHandler(UserManager<AppUser> userManager)
    : IRequestHandler<ValidateEmailAndPhoneCommand, ValidateEmailAndPhoneResult>
{
    public async Task<ValidateEmailAndPhoneResult> Handle(ValidateEmailAndPhoneCommand command, CancellationToken cancellationToken)
    {
        bool phoneRegisterd = await userManager.Users.AnyAsync(u => u.PhoneNumber == command.PhoneNumber);
        bool emailRegistered = await userManager.Users.AnyAsync(u => u.Email == command.Email);

        return new ValidateEmailAndPhoneResult
        {
            EmailRegisterd = emailRegistered,
            PhoneRegisterd = phoneRegisterd
        };
    }
}
