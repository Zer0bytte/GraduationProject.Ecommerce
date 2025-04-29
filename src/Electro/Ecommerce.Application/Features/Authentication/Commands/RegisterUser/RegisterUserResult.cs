namespace Ecommerce.Application.Features.Authentication.Commands.RegisterUser;

public class RegisterUserResult
{
    public string UserName { get; set; } = default!;
    public Guid UserId { get; set; }
    public List<string> Errors { get; set; } = [];

}
