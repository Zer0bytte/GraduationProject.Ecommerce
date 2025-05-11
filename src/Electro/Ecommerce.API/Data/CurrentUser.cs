using BuildingBlocks.Exceptions.Abstractions;
using Ecommerce.Application.Common.Enums;
using Ecommerce.Application.Common.Interfaces;
using System.Security.Claims;

namespace Ecommerce.API.Data;

public class CurrentUser : ICurrentUser
{
    private readonly ClaimsPrincipal? _user;
    private readonly bool _isAuthenticated;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _user = httpContextAccessor.HttpContext?.User;
        _isAuthenticated = httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }

    private CurrentUser(HttpContext context)
    {
        _user = context?.User;
        _isAuthenticated = context?.User?.Identity?.IsAuthenticated ?? false;
    }

    public static CurrentUser Create(HttpContext context)
    {
        return new CurrentUser(context);
    }


    public Guid Id
    {
        get
        {
            if (!_isAuthenticated) return Guid.Empty;
            string? claim = _user?.FindFirstValue(ClaimTypes.NameIdentifier);
            return claim == null
                ? throw new NotFoundException("Invalid user, user id missing in token.")
                : Guid.Parse(claim);
        }
    }

    public string Email
    {
        get
        {
            if (!_isAuthenticated) return string.Empty;
            string? claim = _user?.FindFirstValue(ClaimTypes.Email);
            return claim == null ? throw new Exception("Invalid user, email missing in token.") : claim;
        }
    }

    public Guid SupplierId
    {
        get
        {
            if (!_isAuthenticated) return Guid.Empty;
            string? claim = _user?.FindFirstValue("SupplierId");
            return claim == null
                ? throw new NotFoundException("This user is not a supplier")
                : Guid.Parse(claim);
        }
    }

    public bool IsSupplier
    {
        get
        {
            if (!_isAuthenticated) return false;
            string? claim = _user?.FindFirstValue("SupplierId");
            return claim != null;

        }
    }

    public bool IsAuthenticated
    {
        get
        {
            return _isAuthenticated;

        }
    }
    public string FullName
    {
        get
        {
            if (!_isAuthenticated) return "";
            string? claim = _user?.FindFirstValue("FullName");
            return claim == null
                  ? throw new NotFoundException("Invalid user, full name missing in token.")
                  : claim;
        }
    }

    public UserTypes UserType
    {
        get
        {
            if (!_isAuthenticated) throw new NotFoundException("Invalid user.");
            string? claim = _user?.FindFirstValue("UserType");
            return claim == null
                  ? throw new NotFoundException("Invalid user, type missing in token.")
                  : (UserTypes)Enum.Parse(typeof(UserTypes), claim);
        }
    }


    public bool IsVerifiedSupplier
    {
        get
        {
            if (!_isAuthenticated) return false;
            string? claim = _user?.FindFirstValue("VerifiedSupplier");
            return claim != null;

        }
    }
}
