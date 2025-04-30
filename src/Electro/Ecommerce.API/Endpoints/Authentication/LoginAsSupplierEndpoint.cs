using Ecommerce.Application.Features.Authentication.Queries.LoginAsSupplier;

namespace Ecommerce.API.Endpoints.Authentication;

public class LoginAsSupplierEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/login-supplier", async (LoginAsSupplierQuery request, ISender sender) =>
        {
            LoginAsSupplierResult result = await sender.Send(request);

            return Results.Ok(ApiResponse<LoginAsSupplierResult>.Success(result, ArabicResponseMessages.Authentication.SupplierLoginSuccess));


        })
            .WithTags("Authentication")
            .WithSummary("Login Supplier")
            .Produces<LoginAsSupplierResult>();
    }

}
