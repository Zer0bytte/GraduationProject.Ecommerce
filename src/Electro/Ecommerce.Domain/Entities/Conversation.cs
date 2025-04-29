using Ecommerce.Domain.Enums;
using Ecommerce.Domain.Shared;

namespace Ecommerce.Domain.Entities;
public class Conversation : BaseEntity
{
    public Guid SupplierId { get; set; }
    public AppUser Supplier { get; set; }
    public Guid UserId { get; set; }
    public AppUser User { get; set; }
    public ICollection<Message> Messages { get; set; } = [];
    public string? LastMessage { get; set; }
    public MessageType LastMessageType { get; set; }
    public DateTime LastMessageTime { get; set; }
}
