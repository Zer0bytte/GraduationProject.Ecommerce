namespace Ecommerce.Application.Features.Authentication.Queries.Login;

public class LoginResult
{
    public string AccessToken { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}
