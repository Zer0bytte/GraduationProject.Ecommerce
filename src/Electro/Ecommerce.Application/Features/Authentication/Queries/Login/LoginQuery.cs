namespace Ecommerce.Application.Features.Authentication.Queries.Login;

public class LoginQuery : IRequest<LoginResult>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
