namespace Ecommerce.Application.Features.Users.Commands.DeleteAdmin;

public class DeleteAdminCommandHandler(UserManager<AppUser> userManager, ICurrentUser ICurrentUser) : IRequestHandler<DeleteAdminCommand, DeleteAdminResult>
{
    public async Task<DeleteAdminResult> Handle(DeleteAdminCommand command, CancellationToken cancellationToken)
    {
        if (command.Id == ICurrentUser.Id) throw new InternalServerException("You can't delete yourself!");

        AppUser? user = await userManager.FindByIdAsync(command.Id.ToString());
        if (user is null || user.LockoutEnd.HasValue || !await userManager.IsInRoleAsync(user, "Admin")) throw new UserNotFoundException(command.Id);



        await userManager.SetLockoutEnabledAsync(user, true);

        await userManager.SetLockoutEndDateAsync(user, DateTime.Today.AddYears(10));

        return new DeleteAdminResult
        {
            IsSuccess = true
        };

    }
}
