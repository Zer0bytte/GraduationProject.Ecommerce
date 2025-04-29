using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Conversations.Queries.GetConversations;
public class GetConversationsResult
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = default!;
    public string? LastMessage { get; set; } = default!;
    public int UnreadCount { get; set; }
    public MessageType LastMessageType { get; set; }
    public DateTime LastMessageTime { get; set; } = default!;
}