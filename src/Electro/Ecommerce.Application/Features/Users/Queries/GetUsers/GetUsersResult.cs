namespace Ecommerce.Application.Features.Users.Queries.GetUsers;

public record GetUsersResult
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public string? FullName { get; set; }
    public bool IsActive { get; set; }
}
