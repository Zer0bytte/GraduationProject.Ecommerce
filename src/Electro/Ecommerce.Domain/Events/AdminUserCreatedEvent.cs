namespace Ecommerce.Domain.Events;
public class AdminUserCreatedEvent
{
    public Guid Id { get; set; }
    public string SetPasswordToken { get; set; } = default!;
    public string Email { get; set; } = default!;
}