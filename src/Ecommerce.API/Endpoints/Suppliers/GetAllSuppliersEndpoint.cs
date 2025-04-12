global using Carter;
using Ecommerce.API.ResponseStructure;
using Ecommerce.Application.Common.Persistance;
using Ecommerce.Application.Features.Suppliers.Queries.GetAllSuppliers;
using MediatR;

namespace Ecommerce.API.Endpoints.Suppliers;

public class GetAllSuppliersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/suppliers", async ([AsParameters] GetAllSuppliersQuery query, ISender sender) =>
        {
            PagedResult<GetAllSuppliersResult> result = await sender.Send(query);
            return Results.Ok(ApiResponse<PagedResult<GetAllSuppliersResult>>.Success(result));

        })
            .RequireAuthorization("Admin")
            .WithTags("Suppliers")
            .WithSummary("Get Suppliers")
            .Produces<PagedResult<GetAllSuppliersResult>>();
    }
}
