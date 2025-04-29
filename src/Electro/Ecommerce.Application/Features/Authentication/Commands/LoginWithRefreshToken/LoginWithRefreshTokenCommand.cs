using MediatR;

namespace Ecommerce.Application.Features.Authentication.Commands.LoginWithRefreshToken;
public record LoginWithRefreshTokenCommand : IRequest<LoginWithRefreshTokenResult>
{
    public string RefreshToken { get; set; } = default!;
}