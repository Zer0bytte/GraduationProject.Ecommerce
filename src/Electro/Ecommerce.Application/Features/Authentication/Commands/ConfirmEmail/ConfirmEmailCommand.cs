namespace Ecommerce.Application.Features.Authentication.Commands.ConfirmEmail;

public class ConfirmEmailCommand : IRequest<ConfirmEmailResult>
{
    public Guid UserId { get; set; }
    public int Code { get; set; }
}
