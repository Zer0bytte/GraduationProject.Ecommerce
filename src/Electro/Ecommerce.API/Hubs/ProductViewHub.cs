using Ecommerce.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

public class ProductViewHub(ICurrentUser currentUser) : Hub
{
    private static Dictionary<string, HashSet<UserViewInfo>> _productViews = new();


    public async Task ViewProduct(string productId)
    {
        string connectionId = Context.ConnectionId;
        string username = currentUser.IsAuthenticated ? currentUser.FullName : "Anonymous User";

        UserViewInfo userViewInfo = new UserViewInfo
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

        List<string> usernames = _productViews[productId].Select(u => u.UserName).ToList();

        await Clients.All.SendAsync("UpdateProductViewCount", productId, usernames);
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var connectionId = Context.ConnectionId;

        foreach (var productId in _productViews.Keys)
        {
            var userToRemove = _productViews[productId].FirstOrDefault(u => u.ConnectionId == connectionId);
            if (userToRemove != null)
            {
                _productViews[productId].Remove(userToRemove);

                var usernames = _productViews[productId].Select(u => u.UserName).ToList();

                await Clients.All.SendAsync("UpdateProductViewCount", productId, _productViews[productId].Count, usernames);
            }
        }

        await base.OnDisconnectedAsync(exception);
    }
}

public class UserViewInfo
{
    public string ConnectionId { get; set; }
    public string UserName { get; set; }
}
