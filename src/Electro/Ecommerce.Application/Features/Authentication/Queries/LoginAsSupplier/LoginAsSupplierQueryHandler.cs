namespace Ecommerce.Application.Features.Authentication.Queries.LoginAsSupplier;
internal sealed class LoginAsSupplierQueryHandler(SignInManager<AppUser> signInManager, IApplicationDbContext context, UserManager<AppUser> userManager, IJwtService jwtService, IBus bus) : IRequestHandler<LoginAsSupplierQuery, LoginAsSupplierResult>
{
    public async Task<LoginAsSupplierResult> Handle(LoginAsSupplierQuery query, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByEmailAsync(query.Email);

        if (user is null) throw new InternalServerException("Wrong email or password");
        if (!user.EmailConfirmed)
        {
            await bus.Publish(new EmailVerificationCodeGeneratedEvent
            {
                Email = user.Email,
                VerificationCode = await userManager.GenerateEmailConfirmationTokenAsync(user)
            });
            throw new EmailNotConfirmedException() { UserId = user.Id };
        }

        SignInResult result = await signInManager.PasswordSignInAsync(user, query.Password, true, false);

        if (result.Succeeded)
        {
            if (!await userManager.IsInRoleAsync(user, "Supplier")) throw new InternalServerException("You aren't a supplier!");

            SupplierProfile? supplierProfile = await context.SupplierProfiles.FindAsync(user.SupplierProfileId);

            if (supplierProfile is null) throw new InternalServerException("You are not a supplier");

            if (!supplierProfile.IsVerified) throw new InternalServerException("We are currently working on your verification process");

            string token = await jwtService.GenerateJwtToken(user);
            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Token = jwtService.GenerateRefreshToken(),
                ExpiresOnUtc = DateTime.UtcNow.AddDays(7)
            };

            context.RefreshTokens.Add(refreshToken);

            return new LoginAsSupplierResult
            {
                AccessToken = token,
                RefreshToken = refreshToken.Token,
                Email = query.Email,
                IsEmailVerified = true
            };
        }

        throw new InternalServerException("Wrong email or password");
    }
}