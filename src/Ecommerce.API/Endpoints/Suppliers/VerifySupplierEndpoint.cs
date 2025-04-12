using Ecommerce.Application.Features.Suppliers.Commands.VerifySupplier;

namespace Ecommerce.API.Endpoints.Suppliers;

public class VerifySupplierEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/suppliers/{id}/verify", async (Guid id, ISender sender) =>
        {
            VerifySupplierResult result = await sender.Send(new VerifySupplierCommand { SupplierId = id });

            return Results.Ok(ApiResponse<VerifySupplierResult>.Success(result, "Supplier verified successfully"));

        })
          .RequireAuthorization("Admin")
          .WithTags("Suppliers")
          .WithSummary("Verify Supplier")
          .Produces<VerifySupplierResult>();
    }
}
