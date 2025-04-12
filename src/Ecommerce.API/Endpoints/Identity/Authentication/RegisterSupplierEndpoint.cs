using Ecommerce.Application.Features.Identity.Authentication.Commands.RegisterSupplier;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Endpoints.Identity.Authentication;

public class RegisterSupplierEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/register-supplier", async ([FromForm] RegisterSupplierCommand command, ISender sender) =>
        {
            RegisterSupplierResult result = await sender.Send(command);
            if (result.Errors.Count > 0)
                return Results.BadRequest(result);

            return Results.Ok(ApiResponse<RegisterSupplierResult>.Success(result, "Registration succeeded, please enter your confirmation code"));

        })
            .DisableAntiforgery()
            .WithTags("Authentication")
            .WithSummary("Register Supplier")
            .Accepts<RegisterSupplierCommand>("multipart/form-data")
            .Produces<RegisterSupplierResult>();
    }
}
