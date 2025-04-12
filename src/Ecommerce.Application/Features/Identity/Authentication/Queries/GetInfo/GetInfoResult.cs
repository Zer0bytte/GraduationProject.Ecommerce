namespace Ecommerce.Application.Features.Identity.Authentication.Queries.GetInfo;

public class GetInfoResult
{
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? PhoneNumber { get; set; }
}
