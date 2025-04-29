namespace Ecommerce.Application.Features.Conversations.Queries.GetConversations;
public class GetConversationsQueryHandler(IApplicationDbContext context, ICurrentUser currentUser) : IRequestHandler<GetConversationsQuery, PagedResult<GetConversationsResult>>
{
    public async Task<PagedResult<GetConversationsResult>> Handle(GetConversationsQuery query, CancellationToken cancellationToken)
    {
        IQueryable<Conversation> source = context.Conversations.Where(c => c.UserId == currentUser.Id || c.SupplierId == currentUser.Id);
        IQueryable<GetConversationsResult> conversations;
        if (currentUser.IsSupplier)
        {
            conversations = source.Select(c => new GetConversationsResult
            {
                Id = c.Id,
                FullName = c.User.FullName,
                LastMessage = c.LastMessage,
                UnreadCount = c.Messages.Count(m => m.ReadOn == null && m.MessageBy == MessageBy.User)
            });
        }
        else
        {
            conversations = source.Select(c => new GetConversationsResult
            {
                Id = c.Id,
                FullName = c.Supplier.SupplierProfile.StoreName,
                UnreadCount = c.Messages.Count(m => m.ReadOn == null && m.MessageBy == MessageBy.Supplier),
                LastMessage = c.LastMessage,
                LastMessageTime = c.LastMessageTime,
                LastMessageType = c.LastMessageType
            });
        }

        conversations = conversations
            .Skip((query.Page - 1) * query.Limit)
            .Take(query.Limit);

        var total = await source.CountAsync();

        return PagedResult<GetConversationsResult>.Create(query, total, conversations);
    }
}