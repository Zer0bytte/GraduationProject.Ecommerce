namespace Ecommerce.Application.Features.Identity.Authentication.Commands.RegisterSupplier;

public class RegisterSupplierCommand : IRequest<RegisterSupplierResult>
{
    public string FullName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string BusinessName { get; set; } = default!;
    public string StoreName { get; set; } = default!;
    public string TaxNumber { get; set; } = default!;
    public string NationalId { get; set; } = default!;
    public IFormFile NationalIdFront { get; set; } = default!;
    public IFormFile NationalIdBack { get; set; } = default!;
    public IFormFile TaxCard { get; set; } = default!;
}
