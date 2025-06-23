using Ecommerce.Application.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;

public class ProductViewHub(ICurrentUser currentUser) : Hub
{
    private static Dictionary<string, HashSet<UserViewInfo>> _productViews = new Dictionary<string, HashSet<UserViewInfo>>();

    public async Task ViewProduct(string productId)
    {
        var connectionId = Context.ConnectionId;
        var username = currentUser.IsAuthenticated ? currentUser.FullName : "Anonymouse User";


        var userViewInfo = new UserViewInfo
        {
            ConnectionId = connectionId,
            UserName = username
        };

        lock (_productViews)
        {
            if (!_productViews.ContainsKey(productId))
            {
                _productViews[productId] = new HashSet<UserViewInfo>();
            }

            _productViews[productId].Add(userViewInfo);
        }

        await Clients.All.SendAsync("UpdateProductViewCount", productId, _productViews[productId].Count);
    }

    public async Task LeaveProduct(string productId)
    {
        var connectionId = Context.ConnectionId;

        lock (_productViews)
        {
            if (_productViews.ContainsKey(productId))
            {
                var userToRemove = _productViews[productId].FirstOrDefault(u => u.ConnectionId == connectionId);
                if (userToRemove != null)
                {
                    _productViews[productId].Remove(userToRemove);
                }
            }
        }

        // Notify clients that the view count has decreased
        await Clients.All.SendAsync("UpdateProductViewCount", productId, _productViews.ContainsKey(productId) ? _productViews[productId].Count : 0);
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        // Remove the user from all product view lists when they disconnect
        foreach (var productId in _productViews.Keys)
        {
            var userToRemove = _productViews[productId].FirstOrDefault(u => u.ConnectionId == Context.ConnectionId);
            if (userToRemove != null)
            {
                _productViews[productId].Remove(userToRemove);
            }
        }

        return base.OnDisconnectedAsync(exception);
    }
}

public class UserViewInfo
{
    public string ConnectionId { get; set; }
    public string UserName { get; set; }
}
