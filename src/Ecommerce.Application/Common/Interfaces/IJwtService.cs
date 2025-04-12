namespace Ecommerce.Application.Common.Interfaces;

public interface IJwtService
{
    Task<string> GenerateJwtToken(AppUser user);
}
