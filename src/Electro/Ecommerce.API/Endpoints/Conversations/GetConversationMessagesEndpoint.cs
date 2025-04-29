using Ecommerce.Application.Features.Conversations.Commands;
using Ecommerce.Application.Features.Conversations.Queries.GetConversationMessages;

namespace Ecommerce.API.Endpoints.Conversations;

public class GetConversationMessagesRequest : PagedQuery;
public class GetConversationMessagesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/conversations/{id}/messages", async ([AsParameters] GetConversationMessagesRequest request, Guid id, ISender sender) =>
        {

            PagedResult<GetConversationMessageResult> result = await sender.Send(new GetConversationMessageQuery
            {
                ConversationId = id,
                Limit = request.Limit,
                Page = request.Page
            });

            return Results.Ok(ApiResponse<PagedResult<GetConversationMessageResult>>.Success(result));

        })
            .RequireAuthorization("UserOrSupplier")
            .WithTags("Conversations")
            .WithSummary("Get Conversation Messages")
            .Produces<PagedResult<GetConversationMessageResult>>();
    }
}
