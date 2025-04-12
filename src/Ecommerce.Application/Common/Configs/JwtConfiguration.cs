namespace Ecommerce.Application.Common.Configs;

public class JwtConfiguration
{
    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public int AccessTokenLifetime { get; set; }
    public int RefreshTokenLifetime { get; set; }
}
