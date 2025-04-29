using Ecommerce.Application.Features.Conversations.Queries.GetConversationMessages;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Features.Conversations.Commands;
public class StartConversationCommandHandler(IApplicationDbContext context, ICurrentUser currentUser, HostingConfig hostingConfig) : IRequestHandler<StartConversationCommand, StartConversationResult>
{
    public async Task<StartConversationResult> Handle(StartConversationCommand command, CancellationToken cancellationToken)
    {
        Conversation? conversation = await context.Conversations.FirstOrDefaultAsync(c => c.UserId == currentUser.Id && c.SupplierId == command.SupplierId);

        string supplierName = (await context.SupplierProfiles.FirstOrDefaultAsync(sp => sp.UserId == command.SupplierId))?.StoreName;

        if (conversation is null)
        {
            conversation = new Conversation
            {
                Id = Guid.NewGuid(),
                UserId = currentUser.Id,
                SupplierId = command.SupplierId,
            };

            ProductMessage? product = await context.Products.Where(p => p.Id == command.ProductId).Select(p => new ProductMessage
            {
                ProductId = p.Id,
                ProductImage = hostingConfig.HostName + "/media/" + p.Images.FirstOrDefault().NameOnServer,
                ProductTitel = p.Title
            }).FirstOrDefaultAsync(cancellationToken);

            if (product is null) throw new NotFoundException("Product", command.ProductId);

            string productJson = JsonConvert.SerializeObject(product);

            conversation.Messages.Add(new Message
            {
                MessageType = MessageType.Product,
                MessagePayload = productJson,

            });
            context.Conversations.Add(conversation);
            await context.SaveChangesAsync(cancellationToken);
        }
        conversation.LastMessageType = MessageType.Product;
        conversation.LastMessageTime = DateTime.UtcNow;
        conversation.LastMessage = null;

        PagedResult<GetConversationMessageResult> conversationMsgs = await new GetConversationMessageQueryHandler(context, currentUser).Handle(new GetConversationMessageQuery
        {
            ConversationId = conversation.Id,

        }, cancellationToken);

        return new StartConversationResult
        {
            SupplierName = supplierName,
            Messages = conversationMsgs,
            ConversationId = conversation.Id
        };

    }
}

public record ProductMessage
{
    public Guid ProductId { get; set; }
    public string ProductTitel { get; set; }
    public string ProductImage { get; set; }
}

public record TextMessage
{
    public string Text { get; set; }
}