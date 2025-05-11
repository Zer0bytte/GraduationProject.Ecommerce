using Microsoft.AspNetCore.Identity.UI.Services;

namespace Ecommerce.Application.EventHandlers;

public class OrderCancelledEventEventConsumer(IApplicationDbContext dbContext) : IConsumer<OrderCancelledEvent>
{
    public async Task Consume(ConsumeContext<OrderCancelledEvent> context)
    {
        var orderItems = await dbContext.OrderItems.Where(oi => oi.OrderId == context.Message.OrderId).ToListAsync();
        foreach (var oi in orderItems)
        {
            oi.Status = OrderItemStatus.Cancelled;
            await dbContext.Products
                .Where(p => p.Id == oi.ProductId)
                .ExecuteUpdateAsync(p => p.SetProperty(
                    product => product.Stock,
                    product => product.Stock + oi.Quantity), CancellationToken.None);
        }
        await dbContext.SaveChangesAsync(CancellationToken.None);
    }
}
