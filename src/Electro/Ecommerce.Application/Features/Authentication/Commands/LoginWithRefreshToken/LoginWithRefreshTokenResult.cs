namespace Ecommerce.Application.Features.Authentication.Commands.LoginWithRefreshToken;

public sealed class LoginWithRefreshTokenResult
{
    public string AccessToken { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}
