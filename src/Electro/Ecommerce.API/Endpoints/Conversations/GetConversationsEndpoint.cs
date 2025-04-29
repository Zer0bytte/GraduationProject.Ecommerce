using Ecommerce.Application.Features.Conversations.Queries.GetConversationMessages;
using Ecommerce.Application.Features.Conversations.Queries.GetConversations;

namespace Ecommerce.API.Endpoints.Conversations;

public class GetConversationsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/conversations/", async ([AsParameters]GetConversationsQuery request, ISender sender) =>
        {

            PagedResult<GetConversationsResult> result = await sender.Send(request);

            return Results.Ok(ApiResponse<PagedResult<GetConversationsResult>>.Success(result));

        })
            .RequireAuthorization("UserOrSupplier")
            .WithTags("Conversations")
            .WithSummary("Get Conversations")
            .Produces<PagedResult<GetConversationsResult>>();
    }
}
