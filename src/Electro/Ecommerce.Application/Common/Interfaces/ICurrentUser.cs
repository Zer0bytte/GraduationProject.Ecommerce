using Ecommerce.Application.Common.Enums;

namespace Ecommerce.Application.Common.Interfaces;

public interface ICurrentUser
{
    string Email { get; }
    string FullName { get; }
    Guid Id { get; }
    Guid SupplierId { get; }
    bool IsSupplier { get; }
    bool IsAuthenticated { get; }
    bool IsVerifiedSupplier { get; }
    UserTypes UserType { get; }

}