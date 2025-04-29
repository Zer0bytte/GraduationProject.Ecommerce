namespace Ecommerce.Application.Features.Authentication.Commands.ValidateEmail;

public record ValidateEmailAndPhoneResult
{
    public bool EmailRegisterd { get; set; }
    public bool PhoneRegisterd { get; set; }
}
