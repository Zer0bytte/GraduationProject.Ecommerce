using ImageProcessor.Common.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace Ecommerce.Application.BackgroundServices;

public class OrderPaymentTimeoutCheckerService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IBus _bus;
    private readonly ILogger<OrderPaymentTimeoutCheckerService> _logger;
    private readonly TimeSpan _period = TimeSpan.FromMinutes(5);

    public OrderPaymentTimeoutCheckerService(
        IServiceScopeFactory scopeFactory,
        IBus bus,
        ILogger<OrderPaymentTimeoutCheckerService> logger)
    {
        _scopeFactory = scopeFactory;
        _bus = bus;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new PeriodicTimer(_period);

        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

                    var now = DateTime.UtcNow;

                    var orders = await context.Orders
                        .Where(o => o.PaymentMethod == PaymentMethod.Online
                                    && o.PaymentStatus == PaymentStatus.Pending
                                    && o.Status != OrderStatus.Cancelled
                                    && o.OrderDate < now.AddMinutes(-10))
                        .ToListAsync(stoppingToken);

                    _logger.LogInformation($"Orders Should be Cancelled Count: {orders.Count}");

                    if (orders.Count > 0)
                    {
                        var orderIds = orders.Select(o => o.Id).ToList();

                        await context.Orders
                            .Where(o => orderIds.Contains(o.Id))
                            .ExecuteUpdateAsync(prop => prop
                                .SetProperty(p => p.Status, OrderStatus.Cancelled)
                                .SetProperty(p => p.PaymentStatus, PaymentStatus.Failed)
                                .SetProperty(p => p.CancellationReason, "Payment timeout"),
                                stoppingToken);

                        await context.OrderItems
                            .Where(oi => orderIds.Contains(oi.OrderId))
                            .ExecuteUpdateAsync(prop => prop
                                .SetProperty(p => p.Status, OrderItemStatus.Cancelled)
                                .SetProperty(p => p.CancellationReason, "Payment timeout"),
                                stoppingToken);

                        foreach (var order in orders)
                        {
                            await _bus.Publish(new OrderCancelledEvent
                            {
                                OrderId = order.Id
                            }, stoppingToken);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking order payment timeout.");
            }
        }
    }
}
