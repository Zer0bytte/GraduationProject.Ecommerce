namespace Ecommerce.Application.Common.Interfaces;

public interface ICurrentUser
{
    string Email { get; }
    Guid Id { get; }
    Guid SupplierId { get; }
}