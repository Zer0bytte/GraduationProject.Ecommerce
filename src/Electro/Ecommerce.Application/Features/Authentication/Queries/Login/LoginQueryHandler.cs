namespace Ecommerce.Application.Features.Authentication.Queries.Login;

public class LoginQueryHandler(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IJwtService jwtService, IBus bus, IApplicationDbContext context) : IRequestHandler<LoginQuery, LoginResult>
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
            RefreshToken refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Token = jwtService.GenerateRefreshToken(),
                ExpiresOnUtc = DateTime.UtcNow.AddDays(7)
            };

            context.RefreshTokens.Add(refreshToken);
            await context.SaveChangesAsync(cancellationToken);
            return new LoginResult
            {
                AccessToken = token,
                RefreshToken = refreshToken.Token,
                Email = query.Email
            };
        }

        throw new ApplicationException("Wrong email or password");

    }
}
