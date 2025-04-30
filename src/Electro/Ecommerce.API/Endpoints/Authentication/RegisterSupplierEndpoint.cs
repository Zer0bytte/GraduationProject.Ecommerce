using Ecommerce.Application.Features.Authentication.Commands.RegisterSupplier;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Endpoints.Authentication;

public class RegisterSupplierEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auth/register-supplier", async ([FromForm] RegisterSupplierCommand command, ISender sender) =>
        {
            RegisterSupplierResult result = await sender.Send(command);
            if (result.Errors.Count > 0)
                return Results.BadRequest(result);

            return Results.Ok(ApiResponse<RegisterSupplierResult>.Success(result, ArabicResponseMessages.Authentication.RegisterSuccess));

        })
            .DisableAntiforgery()
            .WithTags("Authentication")
            .WithSummary("Register Supplier")
            .Accepts<RegisterSupplierCommand>("multipart/form-data")
            .Produces<RegisterSupplierResult>();
    }
}
