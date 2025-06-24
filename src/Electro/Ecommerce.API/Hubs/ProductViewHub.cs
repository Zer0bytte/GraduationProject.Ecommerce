using Ecommerce.Application.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;

public class ProductViewHub(ICurrentUser currentUser) : Hub
{
    private static Dictionary<string, HashSet<UserViewInfo>> _productViews = new();


    public async Task ViewProduct(string productId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, productId);

        string connectionId = Context.ConnectionId;
        string? username = currentUser.IsAuthenticated ? currentUser.FullName : null;

        UserViewInfo userViewInfo = new UserViewInfo
        {
            ConnectionId = connectionId,
            UserName = username,
            UserId = currentUser.IsAuthenticated ? currentUser.Id : null
        };

        lock (_productViews)
        {
            if (!_productViews.ContainsKey(productId))
            {
                _productViews[productId] = new HashSet<UserViewInfo>();
            }
            if (currentUser.IsAuthenticated)
            {
                if (!_productViews[productId].Any(u => u.UserId == currentUser.Id))
                {
                    _productViews[productId].Add(userViewInfo);
                }

            }
            else
                _productViews[productId].Add(userViewInfo);
        }

        List<string> usernames = _productViews[productId].Select(u => u.UserName).ToList();

        await Clients.Group(productId).SendAsync("UpdateProductViewCount", usernames);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        string connectionId = Context.ConnectionId;

        foreach (string productId in _productViews.Keys)
        {
            UserViewInfo? userToRemove = _productViews[productId].FirstOrDefault(u => u.ConnectionId == connectionId);
            if (userToRemove != null)
            {
                _productViews[productId].Remove(userToRemove);

                List<string?> usernames = _productViews[productId].Select(u => u.UserName).ToList();

                await Clients.Group(productId).SendAsync("UpdateProductViewCount", usernames);
            }
        }

        await base.OnDisconnectedAsync(exception);
    }
}

public class UserViewInfo
{
    public string ConnectionId { get; set; }
    public Guid? UserId { get; set; } = null;
    public string? UserName { get; set; }

}
