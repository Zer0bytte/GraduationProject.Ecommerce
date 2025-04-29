using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ecommerce.Application.Features.Conversations.Queries.GetConversationMessages;
public class GetConversationMessageQuery : PagedQuery, IRequest<PagedResult<GetConversationMessageResult>>
{
    public Guid ConversationId { get;  set; }
}