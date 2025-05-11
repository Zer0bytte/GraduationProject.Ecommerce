using Ecommerce.Application.Features.Authentication.Commands.SetPassword;

namespace Ecommerce.API.Endpoints.Authentication;

public class SetPasswordEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/reset-password", async (SetPasswordCommand command, ISender sender) =>
        {
            SetPasswordResult result = await sender.Send(command);
            return Results.Ok(ApiResponse<SetPasswordResult>.Success(result, "تم اعادة تعيين كلمة المرور بنجاح"));
        })
           .WithTags("Authentication")
           .WithSummary("Set Password")
           .Produces<ApiResponse<SetPasswordResult>>();
    }
}