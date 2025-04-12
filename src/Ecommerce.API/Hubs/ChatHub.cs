using Ecommerce.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Ecommerce.API.Hubs;

[Authorize]
public class ChatHub(ICurrentUser currentUser) : Hub
{
    public async Task SendMessage(string receiverId, string message)
    {
        await Clients.Group(receiverId).SendAsync("ReceiveMessage", currentUser.Id, message);
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

