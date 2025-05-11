using Ecommerce.Application.Features.Authentication.Commands.ResetPassword;

namespace Ecommerce.API.Endpoints.Authentication;

public class ResetPasswordEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/reset-password-token", async (ResetPasswordCommand command, ISender sender) =>
        {
            ResetPasswordResult result = await sender.Send(command);
            return Results.Ok(ApiResponse<ResetPasswordResult>.Success(result, "تم ارسال كود التأكيد الي البريد الخاص بك"));
        })
           .WithTags("Authentication")
           .WithSummary("Reset Password Token")
           .Produces<ApiResponse<ResetPasswordResult>>();
    }
}
