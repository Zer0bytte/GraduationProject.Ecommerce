using Ecommerce.Application.Features.Conversations.Queries.GetConversationMessages;

namespace Ecommerce.Application.Features.Conversations.Commands;
public record StartConversationResult
{
    public string SupplierName { get; set; }
    public Guid ConversationId { get; set; }
    public PagedResult<GetConversationMessageResult> Messages { get; set; }


}
