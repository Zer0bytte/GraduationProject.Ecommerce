namespace Ecommerce.Application.Features.Users.Commands.DeleteAdmin;

public record DeleteAdminCommand : IRequest<DeleteAdminResult>
{
    public Guid Id { get; set; }
}
