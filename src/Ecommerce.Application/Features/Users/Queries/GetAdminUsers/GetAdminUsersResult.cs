﻿namespace Ecommerce.Application.Features.Users.Queries.GetAdminUsers;

public record GetAdminUsersResult
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public string? FullName { get; set; }
}
