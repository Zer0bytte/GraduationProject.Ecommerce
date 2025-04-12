using Ecommerce.Application.Features.Identity.Authentication.Queries.LoginAsSupplier;

namespace Ecommerce.API.Endpoints.Identity.Authentication;

public class LoginAsSupplierEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/login-supplier", async (LoginAsSupplierQuery request, ISender sender) =>
        {
            LoginAsSupplierResult result = await sender.Send(request);

            return Results.Ok(ApiResponse<LoginAsSupplierResult>.Success(result, "Supplier logged in successfully."));


        })
            .WithTags("Authentication")
            .WithSummary("Login Supplier")
            .Produces<LoginAsSupplierResult>();
    }

}
