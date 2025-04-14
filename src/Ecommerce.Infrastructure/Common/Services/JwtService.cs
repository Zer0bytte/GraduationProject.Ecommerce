using Ecommerce.Application.Common.Configs;
using Ecommerce.Application.Common.Enums;
using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Infrastructure.Common.Services;
public class JwtService(JwtConfiguration jwtConfig, UserManager<AppUser> userManager, IApplicationDbContext dbContext) : IJwtService
{
    private async Task<SupplierProfile?> GetSupplierIdByUserIdAsync(Guid userId)
    {
        SupplierProfile? supplier = await dbContext.SupplierProfiles
            .FirstOrDefaultAsync(s => s.UserId == userId);

        return supplier;
    }
    public async Task<string> GenerateJwtToken(AppUser user)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        UserTypes userType = await userManager.IsInRoleAsync(user, "Admin") ? UserTypes.Admin :
                             await userManager.IsInRoleAsync(user, "Supplier") ? UserTypes.Supplier :
                             UserTypes.User;

        List<Claim> claims = new List<Claim>
        {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("FullName", user.FullName),
                    new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                    new Claim("UserType", userType.ToString())
        };

        if (userType == UserTypes.Supplier)
        {
            var supplier = await GetSupplierIdByUserIdAsync(user.Id);
            if (supplier is not null)
            {
                claims.Add(new Claim("SupplierId", supplier.Id.ToString()));
                claims.Add(new Claim("VerifiedSupplier", supplier.IsVerified.ToString()));
            }
        }

        byte[] tokenKey = Encoding.UTF8.GetBytes(jwtConfig.Key);
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(jwtConfig.AccessTokenLifetime),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}