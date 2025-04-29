namespace Ecommerce.Application.Features.Authentication.Commands.RegisterSupplier;

public record RegisterSupplierResult
{
    public string UserName { get; set; } = default!;
    public Guid UserId { get; set; }
    public List<string> Errors { get; set; } = [];
}
