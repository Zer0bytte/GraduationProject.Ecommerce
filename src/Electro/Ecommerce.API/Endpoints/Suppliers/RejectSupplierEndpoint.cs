using Ecommerce.Application.Features.Suppliers.Commands.RejectSupplier;
using Ecommerce.Application.Features.Suppliers.Commands.VerifySupplier;

namespace Ecommerce.API.Endpoints.Suppliers;

public class RejectSupplierEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/suppliers/{id}/reject", async (Guid id, RejectSupplierCommand command, ISender sender) =>
        {
            command.SupplierId = id;
            RejectSupplierResult result = await sender.Send(command);

            return Results.Ok(ApiResponse<RejectSupplierResult>.Success(result, "Supplier rejected successfully"));

        })
          .RequireAuthorization("Admin")
          .WithTags("Suppliers")
          .WithSummary("Reject Supplier")
          .Produces<VerifySupplierResult>();
    }
}
