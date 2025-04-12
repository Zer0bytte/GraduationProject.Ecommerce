namespace Ecommerce.Application.Features.Identity.Authentication.Commands.RegisterUser;

public class RegisterUserCommand : IRequest<RegisterUserResult>
{
    public string FullName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
}
