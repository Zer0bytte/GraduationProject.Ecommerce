using Ecommerce.Application.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;

public class ProductViewHub(ICurrentUser currentUser) : Hub
{
    private static Dictionary<string, HashSet<UserViewInfo>> _productViews = new Dictionary<string, HashSet<UserViewInfo>>();

    public async Task ViewProduct(string productId)
    {
        var connectionId = Context.ConnectionId;
        var username = currentUser.IsAuthenticated ? currentUser.FullName : "Anonymous User";

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

        var usernames = _productViews[productId].Select(u => u.UserName).ToList();

        await Clients.All.SendAsync("UpdateProductViewCount", productId, usernames);
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

        var usernames = _productViews.ContainsKey(productId) ? _productViews[productId].Select(u => u.UserName).ToList() : new List<string>();

        await Clients.All.SendAsync("UpdateProductViewCount", productId, _productViews.ContainsKey(productId) ? _productViews[productId].Count : 0, usernames);
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
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
