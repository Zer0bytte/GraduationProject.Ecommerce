namespace Ecommerce.Application.Features.Identity.Authentication.Queries.LoginAsSupplier;

public sealed class LoginAsSupplierResult
{
    public string AccessToken { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool IsEmailVerified { get; set; }
}
