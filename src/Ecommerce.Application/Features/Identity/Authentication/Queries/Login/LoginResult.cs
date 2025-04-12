namespace Ecommerce.Application.Features.Identity.Authentication.Queries.Login;

public class LoginResult
{
    public string AccessToken { get; set; } = default!;
    public string Email { get; set; } = default!;
}
