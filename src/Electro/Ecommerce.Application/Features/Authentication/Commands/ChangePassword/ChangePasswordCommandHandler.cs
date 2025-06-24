namespace Ecommerce.Application.Features.Authentication.Commands.ChangePassword;
public class ChangePasswordCommandHandler(UserManager<AppUser> userManager, ICurrentUser currentUser, IApplicationDbContext context, IJwtService jwtService) : IRequestHandler<ChangePasswordCommand, ChangePasswordResult>
{
    public async Task<ChangePasswordResult> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        AppUser? user = await context.Users.FindAsync(currentUser.Id);
        if (user is null) throw new UserNotFoundException();

        IdentityResult result = await userManager.ChangePasswordAsync(user, command.CurrentPassword, command.NewPassword);
        if (!result.Succeeded)
            throw new ApplicationException("حدث خطأ, برجاء المحاوله لاحقا");

        await context.RefreshTokens
            .Where(r => r.UserId == currentUser.Id)
            .ExecuteUpdateAsync(p => p
                .SetProperty(r => r.ExpiresOnUtc, r => DateTime.UtcNow.AddDays(-1)), cancellationToken);

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
        return new ChangePasswordResult
        {
            AccessToken = token,
            RefreshToken = refreshToken.Token
        };
    }
}