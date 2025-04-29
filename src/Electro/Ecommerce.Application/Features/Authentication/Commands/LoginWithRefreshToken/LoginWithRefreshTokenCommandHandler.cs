namespace Ecommerce.Application.Features.Authentication.Commands.LoginWithRefreshToken;
internal sealed class LoginWithRefreshTokenCommandHandler(IJwtService jwtService, IApplicationDbContext context) : IRequestHandler<LoginWithRefreshTokenCommand, LoginWithRefreshTokenResult>
{
    public async Task<LoginWithRefreshTokenResult> Handle(LoginWithRefreshTokenCommand command, CancellationToken cancellationToken)
    {
        RefreshToken? refreshToken = await context.RefreshTokens.Where(r => r.Token == command.RefreshToken)
            .Include(r => r.User).FirstOrDefaultAsync();

        if (refreshToken is null || refreshToken.ExpiresOnUtc < DateTime.UtcNow)
        {
            throw new ApplicationException("The refresh token was expired.");
        }

        string accessToken = await jwtService.GenerateJwtToken(refreshToken.User);

        refreshToken.Token = jwtService.GenerateRefreshToken();
        refreshToken.ExpiresOnUtc = DateTime.UtcNow.AddDays(7);
        await context.SaveChangesAsync(cancellationToken);
        return new LoginWithRefreshTokenResult
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            Email = refreshToken.User.Email
        };
    }
}