namespace Ecommerce.Application.Features.Identity.Authentication.Commands.ValidateEmail;

public record ValidateEmailAndPhoneCommand : IRequest<ValidateEmailAndPhoneResult
    >
{
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
}
