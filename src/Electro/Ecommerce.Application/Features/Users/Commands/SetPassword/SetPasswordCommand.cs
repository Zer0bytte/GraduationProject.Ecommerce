namespace Ecommerce.Application.Features.Users.Commands.SetPassword;

public record SetPasswordCommand : IRequest<SetPasswordResult>
{
    public Guid UserId { get; set; }
    public string Password { get; set; } = default!;
    public string Token { get; set; } = default!;
}
