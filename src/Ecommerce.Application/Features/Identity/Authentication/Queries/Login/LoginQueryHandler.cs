namespace Ecommerce.Application.Features.Identity.Authentication.Queries.Login;

public class LoginQueryHandler(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IJwtService jwtService, IBus bus) : IRequestHandler<LoginQuery, LoginResult>
{
    public async Task<LoginResult> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByEmailAsync(query.Email);
        if (user is null) throw new InternalServerException("Wrong email or password");
        if (!user.EmailConfirmed)
        {
            await bus.Publish(new EmailVerificationCodeGeneratedEvent
            {
                Email = user.Email,
                VerificationCode = await userManager.GenerateEmailConfirmationTokenAsync(user)
            });
            throw new EmailNotConfirmedException() { UserId = user.Id };
        }

        SignInResult result = await signInManager.PasswordSignInAsync(user, query.Password, true, false);
        if (result.Succeeded)
        {
            string token = await jwtService.GenerateJwtToken(user);
            return new LoginResult
            {
                AccessToken = token,
                Email = query.Email
            };
        }

        throw new InternalServerException("Wrong email or password");

    }
}
