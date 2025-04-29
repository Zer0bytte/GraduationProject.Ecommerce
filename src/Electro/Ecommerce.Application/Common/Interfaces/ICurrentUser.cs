namespace Ecommerce.Application.Common.Interfaces;

public interface ICurrentUser
{
    string Email { get; }
    string FullName { get; }
    Guid Id { get; }
    Guid SupplierId { get; }
    bool IsSupplier { get; }
    string UserType { get; }

}