using Ecommerce.Application.Features.Charts.GetCurrentSupplier;
using Ecommerce.Application.Features.Suppliers.Queries.GetAllSuppliers;

namespace Ecommerce.API.Endpoints.Suppliers;

public class GetCurrentSupplierEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/suppliers/me", async (ISender sender) =>
        {
            GetCurrentSupplierResult result = await sender.Send(new GetCurrentSupplierQuery());
            return Results.Ok(ApiResponse<GetCurrentSupplierResult>.Success(result));

        })
            .RequireAuthorization("Supplier")
            .WithTags("Suppliers")
            .WithSummary("Get Me")
            .Produces<ApiResponse<GetCurrentSupplierResult>>();
    }
}
