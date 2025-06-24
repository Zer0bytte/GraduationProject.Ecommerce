using Ecommerce.Application.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;

public class ProductViewHub(ICurrentUser currentUser) : Hub
{
    private static readonly Dictionary<string, HashSet<string>> _productViews = new();


    public async Task ViewProduct(string productId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, productId);

        var random = new Random();
        string username = currentUser.IsAuthenticated ? currentUser.FullName : $"Anonymous + {random.Next(1, 99)}";

        lock (_productViews)
        {
            if (!_productViews.ContainsKey(productId))
            {
                _productViews[productId] = new HashSet<string>();
            }

            _productViews[productId].Add(username);
        }

        await Clients.Group(productId).SendAsync("UpdateProductViewCount", _productViews[productId].ToList());

    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        string? username = currentUser.IsAuthenticated ? currentUser.FullName : null;

        if (username == null)
        {
            await base.OnDisconnectedAsync(exception);
            return;
        }

        lock (_productViews)
        {
            foreach (var productId in _productViews.Keys)
            {
                if (_productViews[productId].Contains(username))
                {
                    _productViews[productId].Remove(username);
                    Clients.Group(productId).SendAsync("UpdateProductViewCount", _productViews[productId].ToList());
                }
            }
        }

        await base.OnDisconnectedAsync(exception);
    }
}

public class UserViewInfo
{
    public string ConnectionId { get; set; }
    public string? UserName { get; set; }
}
