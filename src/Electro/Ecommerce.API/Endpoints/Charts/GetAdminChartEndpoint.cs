using Ecommerce.Application.Features.Charts.GetCurrentSupplier;
using Ecommerce.Application.Features.Charts.GetGlobalCharts;

namespace Ecommerce.API.Endpoints.Charts;

public class GetAdminChartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/admin/chart", async (ISender sender) =>
        {
            GetGlobalChartsResult result = await sender.Send(new GetGlobalChartsQuery());
            return Results.Ok(ApiResponse<GetGlobalChartsResult>.Success(result));

        })
            .WithTags("Admins")
            .WithSummary("Get Chart")
            .Produces<ApiResponse<GetGlobalChartsResult>>();
    }
}
