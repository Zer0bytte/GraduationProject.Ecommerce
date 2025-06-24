using Ecommerce.Application.Features.Authentication.Commands.ChangePassword;

namespace Ecommerce.API.Endpoints.Authentication;

public class ChangePasswordEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/change-password", async (ChangePasswordCommand command, ISender sender) =>
        {
            ChangePasswordResult result = await sender.Send(command);
            return Results.Ok(ApiResponse<ChangePasswordResult>.Success(result, "تم تحديث كلمة المرور بنجاح, برجاء اعادة تسجيل الدخول"));
        })
            .RequireAuthorization()
            .WithTags("Authentication")
            .WithSummary("Change Password")
            .Produces<ApiResponse<ChangePasswordResult>>();
    }
}
