using Ecommerce.Application.Features.Suppliers.Queries.GetAllSuppliers;
using Ecommerce.Application.Features.Suppliers.Queries.GetSupplierById;

namespace Ecommerce.API.Endpoints.Suppliers;

public class GetSupplierByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/suppliers/{id}", async (Guid id, ISender sender) =>
        {
            GetSupplierByIdResult result = await sender.Send(new GetSupplierByIdQuery { Id = id });
            return Results.Ok(ApiResponse<GetSupplierByIdResult>.Success(result));

        })
            //.RequireAuthorization("Admin")
            .WithTags("Suppliers")
            .WithSummary("Get Supplier By Id")
            .Produces<GetSupplierByIdResult>();
    }
}

