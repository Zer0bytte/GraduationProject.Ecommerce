namespace Ecommerce.Application.Features.Identity.Authentication.Queries.LoginAsSupplier;

public record LoginAsSupplierQuery : IRequest<LoginAsSupplierResult>
{
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;

}