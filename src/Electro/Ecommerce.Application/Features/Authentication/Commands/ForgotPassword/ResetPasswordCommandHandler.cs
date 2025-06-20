namespace Ecommerce.Application.Features.Authentication.Commands.ForgotPassword;
public class ResetPasswordCommandHandler(UserManager<AppUser> userManager, IBus bus) : IRequestHandler<ResetPasswordCommand, ResetPasswordResult>
{
    public async Task<ResetPasswordResult> Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByEmailAsync(command.Email);
        if (user is null) throw new NotFoundException("برجاء المحاولة لاحقا");

        string resetPasswordToken = await userManager.GeneratePasswordResetTokenAsync(user);

        await bus.Publish(new ResetPasswordCodeGeneratedEvent
        {
            Email = user.Email!,
            ResetToken = resetPasswordToken
        });

        return new ResetPasswordResult
        {
            UserId = user.Id
        };
    }
}