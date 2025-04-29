namespace Ecommerce.Application.Features.Users.Commands.SetPassword;

public record SetPasswordResult
{
    public bool IsSuccess { get; set; }
    public List<string> Errors { get; set; } = [];

}
