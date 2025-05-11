namespace Ecommerce.Application.Features.Users.Commands.DeleteAdmin;

public record DeactivateUserCommand : IRequest<DeactivateUserResult>
{
    public Guid Id { get; set; }
}
