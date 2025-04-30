using Ecommerce.Application.Features.Authentication.Queries.Login;
using Google.Apis.Auth;

namespace Ecommerce.Application.Features.Authentication.Commands.LoginWithGoogle;
public class LoginWithGoogleCommandHandler(UserManager<AppUser> userManager, IJwtService jwtService, IApplicationDbContext context) : IRequestHandler<LoginWithGoogleCommand, LoginWithGoogleResult>
{
    public async Task<LoginWithGoogleResult> Handle(LoginWithGoogleCommand command, CancellationToken cancellationToken)
    {
        GoogleJsonWebSignature.Payload payload = await GoogleJsonWebSignature.ValidateAsync(command.Credentials, new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new[] { "374485313097-nmsu905obpruikibprsnlf4ls231toh1.apps.googleusercontent.com" }
        });
        AppUser? user = await userManager.FindByEmailAsync(payload.Email);
        if (user is null)
        {
            user = new AppUser
            {
                Email = payload.Email,
                FullName = payload.Name,
                EmailConfirmed = true,
                UserName = payload.Email
            };
            var result = await userManager.CreateAsync(user);
            await userManager.AddToRoleAsync(user, "User");

            if (!result.Succeeded)
                throw new ApplicationException("حدث خطا, برجاء المحاوله لاحقا");
        }
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
        return new LoginWithGoogleResult
        {
            AccessToken = token,
            RefreshToken = refreshToken.Token,
            Email = payload.Email
        };


    }
}