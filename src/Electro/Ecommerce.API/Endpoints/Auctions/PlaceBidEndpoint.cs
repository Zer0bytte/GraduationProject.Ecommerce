using Ecommerce.Application.Features.Auctions.Commands.PlaceBid;

namespace Ecommerce.API.Endpoints.Auctions;

public class PlaceBidEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/auctions/place-bid", async (PlaceBidCommand request, ISender sender) =>
        {
            PlaceBidResult result = await sender.Send(request);
            return Results.Ok(ApiResponse<PlaceBidResult>.Success(result, ArabicResponseMessages.Auction.PlaceBid));

        })
            .RequireAuthorization("User")
            .WithTags("Auctions")
            .WithSummary("Place A Bid")
            .Produces<ApiResponse<PlaceBidResult>>();
    }
}
