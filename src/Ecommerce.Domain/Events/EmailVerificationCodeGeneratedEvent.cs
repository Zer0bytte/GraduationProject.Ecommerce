namespace Ecommerce.Domain.Events;

public class EmailVerificationCodeGeneratedEvent
{
    public string VerificationCode { get; set; } = default!;
    public string Email { get; set; } = default!;
}
