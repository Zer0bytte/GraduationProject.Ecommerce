using Ecommerce.Domain.Enums;
using Ecommerce.Domain.Shared;

namespace Ecommerce.Domain.Entities;

public class Message : BaseEntity
{
    public Guid ConversationId { get; set; }
    public Conversation Conversation { get; set; }
    public string MessagePayload { get; set; } = default!;
    public MessageType MessageType { get; set; }
    public MessageBy MessageBy { get; set; }
    public DateTime? ReadOn { get; set; }
    public DateTime SentOn { get; set; } = DateTime.UtcNow;

}
public enum MessageBy
{
    User,
    Supplier
}