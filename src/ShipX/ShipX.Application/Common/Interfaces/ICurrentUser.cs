namespace ShipX.Application.Common.Interfaces;

public interface ICurrentUser
{
    string Email { get; }
    Guid Id { get; }
}