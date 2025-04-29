using FluentValidation;

namespace Ecommerce.Application.Features.Authentication.Commands.LoginWithRefreshToken;
public class LoginWithRefreshTokenCommandValidator : AbstractValidator<LoginWithRefreshTokenCommand>
{
    public LoginWithRefreshTokenCommandValidator()
    {
        // Add validation rules here
    }
}
