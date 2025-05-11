using Ecommerce.Application.Common.Enums;

namespace Ecommerce.Application.Features.Authentication.Queries.GetInfo;

public class GetInfoResult
{
    public string FullName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public UserTypes Role { get; set; } = default!;
}
