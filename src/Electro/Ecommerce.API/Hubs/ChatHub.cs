using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Features.Conversations.Commands;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace Ecommerce.API.Hubs;

[Authorize]
public class ChatHub(ICurrentUser currentUser, IApplicationDbContext context) : Hub
{
    public async Task SendMessage(MessagePayload payload)
    {
        var conversation = await context.Conversations.FindAsync(payload.ConversationId);
        Guid receiverId;
        if (currentUser.IsSupplier)
        {
            receiverId = conversation.UserId;
        }
        else
        {
            receiverId = conversation.SupplierId;
        }
        await Clients.Group(receiverId.ToString()).SendAsync("ReceiveMessage", new MessageModel
        {
            IsIncoming = true,
            MessageType = MessageType.Text,
            Payload = new TextMessage
            {
                Text = payload.Message,
            },
        });

        context.Messages.Add(new Domain.Entities.Message
        {
            ConversationId = payload.ConversationId,
            MessageType = Domain.Enums.MessageType.Text,
            MessagePayload = JsonConvert.SerializeObject(new TextMessage
            {
                Text = payload.Message
            }),
            MessageBy = currentUser.IsSupplier ? MessageBy.Supplier : MessageBy.User
        });
        conversation.LastMessage = payload.Message;
        conversation.LastMessageType = MessageType.Text;
        conversation.LastMessageTime = DateTime.UtcNow;
        await context.SaveChangesAsync(CancellationToken.None);
    }

    public override async Task OnConnectedAsync()
    {
        Guid userId = currentUser.Id;
        await Groups.AddToGroupAsync(Context.ConnectionId, userId.ToString());
        await base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}

public class MessageModel
{
    public MessageType MessageType { get; set; }
    public DateTime? ReadOn { get; set; }
    public DateTime SentOn { get; set; } = DateTime.UtcNow;
    public TextMessage Payload { get; set; }
    public bool IsIncoming { get; set; }

}
public class MessagePayload
{
    public Guid ConversationId { get; set; }
    public string Message { get; set; } = default!;
}