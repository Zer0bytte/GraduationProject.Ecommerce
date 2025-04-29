namespace Ecommerce.Application.Features.Users.Commands.AddAdminUser;

public class AddAdminCommand : IRequest<AddAdminResult>
{
    public string Email { get; set; } = default!;
    public string FullName { get; set; } = default!;
    public string? PhoneNumber { get; set; }
}
