namespace Ecommerce.Application.Features.Conversations.Commands;
public class StartConversationCommand : IRequest<StartConversationResult>
{
    public Guid SupplierId { get; set; }
    public Guid ProductId { get; set; }
}