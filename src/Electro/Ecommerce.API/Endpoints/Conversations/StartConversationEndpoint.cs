using Ecommerce.Application.Features.Conversations.Commands;
using Ecommerce.Application.Features.CouponCodes;

namespace Ecommerce.API.Endpoints.Conversations;

public class StartConversationEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/conversations/start", async (StartConversationCommand command, ISender sender) =>
        {
            StartConversationResult result = await sender.Send(command);

            return Results.Ok(ApiResponse<StartConversationResult>.Success(result, ArabicResponseMessages.Conversations.Started));

        })
            .RequireAuthorization("User")
            .WithTags("Conversations")
            .WithSummary("Start Conversation")
            .Produces<StartConversationResult>();
    }
}
