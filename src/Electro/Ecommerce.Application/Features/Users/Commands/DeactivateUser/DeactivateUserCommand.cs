namespace Ecommerce.Application.Features.Users.Commands.DeactivateUser;

public record DeactivateUserCommand : IRequest<DeactivateUserResult>
{
    public Guid Id { get; set; }
}
