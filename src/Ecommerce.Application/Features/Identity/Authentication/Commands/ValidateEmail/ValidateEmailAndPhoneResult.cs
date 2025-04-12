namespace Ecommerce.Application.Features.Identity.Authentication.Commands.ValidateEmail;

public record ValidateEmailAndPhoneResult
{
    public bool EmailRegisterd { get; set; }
    public bool PhoneRegisterd { get; set; }
}
