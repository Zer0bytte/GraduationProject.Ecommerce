using System.Text;

namespace Ecommerce.Application.Features.Users.Commands.AddAdminUser;

public class AddAdminCommandHandler(UserManager<AppUser> userManager, IBus bus) : IRequestHandler<AddAdminCommand, AddAdminResult>
{
    public async Task<AddAdminResult> Handle(AddAdminCommand command, CancellationToken cancellationToken)
    {
        if (await userManager.FindByEmailAsync(command.Email) != null) throw new EmailAlreadyExistsException();
        AppUser user = new AppUser
        {
            UserName = command.Email,
            Email = command.Email,
            FullName = command.FullName,
            PhoneNumber = command.PhoneNumber,
            EmailConfirmed = true
        };
        IdentityResult registerationResult = await userManager.CreateAsync(user);
        if (registerationResult.Succeeded)
        {
            string token = await userManager.GeneratePasswordResetTokenAsync(user);
            await userManager.AddToRoleAsync(user, "Admin");

            var plainTextBytes = Encoding.UTF8.GetBytes(token);
            token = Convert.ToBase64String(plainTextBytes);

            await bus.Publish(new AdminUserCreatedEvent
            {
                Email = user.Email,
                SetPasswordToken = token,
                Id = user.Id
            });
        }

        return new AddAdminResult
        {
            UserId = user.Id,
        };
    }

}
