using ShipX.Domain.Entities;

namespace ShipX.Application.Common.Interfaces;

public interface IJwtService
{
    Task<string> GenerateJwtToken(AppUser user);
    string GenerateRefreshToken();

}
