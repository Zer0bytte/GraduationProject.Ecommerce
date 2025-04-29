namespace Ecommerce.Application.Features.Authentication.Commands.RegisterUser;

public class RegisterUserCommandHandler(UserManager<AppUser> userManager, IBus bus, bool isSupplier = false) : IRequestHandler<RegisterUserCommand, RegisterUserResult>
{
    public async Task<RegisterUserResult> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        AppUser? userExist = await userManager.FindByEmailAsync(command.Email);
        bool phoneNumberExist = await userManager.Users.AnyAsync(u => u.PhoneNumber == command.PhoneNumber);
        if (userExist is not null) throw new EmailAlreadyExistsException();
        if (phoneNumberExist) throw new PhoneNumberAlreadyExistsException();

        AppUser user = new AppUser
        {
            UserName = command.Email,
            Email = command.Email,
            FullName = command.FullName,
            PhoneNumber = command.PhoneNumber,
        };

        IdentityResult registerationResult = await userManager.CreateAsync(user, command.Password);
        if (registerationResult.Succeeded)
        {
            if (isSupplier)
                await userManager.AddToRoleAsync(user, "Supplier");
            else
                await userManager.AddToRoleAsync(user, "User");

            string confirmationCode = await userManager.GenerateEmailConfirmationTokenAsync(user);
            await bus.Publish(new EmailVerificationCodeGeneratedEvent
            {
                Email = user.Email,
                VerificationCode = confirmationCode,
                Name = user.FullName.Split(' ')[0]
            });

            return new RegisterUserResult
            {
                UserName = user.UserName,
                UserId = user.Id
            };
        }

        List<string> errors = registerationResult.Errors.Select(e => e.Description).ToList();


        return new RegisterUserResult
        {
            Errors = errors
        };
    }


}
