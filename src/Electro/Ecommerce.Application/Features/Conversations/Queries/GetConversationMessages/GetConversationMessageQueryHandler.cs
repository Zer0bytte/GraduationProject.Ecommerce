

using Ecommerce.Application.Features.Conversations.Commands;
using Ecommerce.Application.Features.Products.Queries.GetAllProducts;

namespace Ecommerce.Application.Features.Conversations.Queries.GetConversationMessages;
public class GetConversationMessageQueryHandler(IApplicationDbContext context, ICurrentUser currentUser) : PagedQuery, IRequestHandler<GetConversationMessageQuery, PagedResult<GetConversationMessageResult>>
{
    public async Task<PagedResult<GetConversationMessageResult>> Handle(GetConversationMessageQuery query, CancellationToken cancellationToken)
    {
        var source = context.Messages
         .Where(m => m.ConversationId == query.ConversationId)
         .Where(m => m.Conversation.UserId == currentUser.Id || m.Conversation.SupplierId == currentUser.Id)
         .OrderByDescending(m => m.CreatedOn)
         .AsQueryable();

        var msgs = source.Select(m => new GetConversationMessageResult
        {
            IsIncoming = currentUser.IsSupplier ? m.MessageBy == MessageBy.User : m.MessageBy == MessageBy.Supplier,
            MessageType = m.MessageType,
            ReadOn = m.ReadOn,
            SentOn = m.SentOn,
            Payload = MapBasedOnMessageType(m.MessageType, m.MessagePayload)
        })
            .Skip((query.Page - 1) * query.Limit)
            .Take(query.Limit).Reverse();


        int total = await source.CountAsync(cancellationToken: cancellationToken);

        return PagedResult<GetConversationMessageResult>.Create(query, total, msgs);

    }

    private static object MapBasedOnMessageType(MessageType messageType, string messagePayload)
    {
        switch (messageType)
        {
            case MessageType.Product:
                return JsonConvert.DeserializeObject<ProductMessage>(messagePayload);
            case MessageType.Text:
                return JsonConvert.DeserializeObject<TextMessage>(messagePayload);

            default:
                return null;
        }
    }
}