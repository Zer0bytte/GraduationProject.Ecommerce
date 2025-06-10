using ImageProcessor.Common.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.BackgroundServices;
public class OrderPaymentTimoutCheckerService : BackgroundService
{
    private readonly IServiceScopeFactory scopeFactory;
    private readonly IBus bus;
    private readonly ILogger<OrderPaymentTimoutCheckerService> _logger;
    private readonly TimeSpan _period = TimeSpan.FromMinutes(5);
    public OrderPaymentTimoutCheckerService(IServiceScopeFactory scopeFactory, IBus bus, ILogger<OrderPaymentTimoutCheckerService> logger)
    {
        this.scopeFactory = scopeFactory;
        this.bus = bus;
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new PeriodicTimer(_period);
        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync())
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

                var orders = await context.Orders.Where(o => o.PaymentMethod == PaymentMethod.Online
                    && o.PaymentStatus == PaymentStatus.Pending && o.Status != OrderStatus.Cancelled
                    && DateTime.Now > o.OrderDate.AddMinutes(10)).ToListAsync(stoppingToken);

                _logger.LogInformation($"Orders Should Cancelled Count :{orders.Count}");
                if (orders.Count > 0)
                {
                    await context.Orders
                        .Include(o => o.OrderItems)
                        .Where(o => o.PaymentMethod == PaymentMethod.Online
                        && o.PaymentStatus == PaymentStatus.Pending
                        && DateTime.Now > o.OrderDate.AddMinutes(10))
                        .ExecuteUpdateAsync(prop => prop
                        .SetProperty(p => p.Status, OrderStatus.Cancelled)
                        .SetProperty(p => p.PaymentStatus, PaymentStatus.Failed)
                        .SetProperty(p => p.CancellationReason, "Payment timeout")
                        .SetProperty(p => p.Status, OrderStatus.Completed),
                        stoppingToken);

                    await context.OrderItems
                    .Where(oi => oi.Order.PaymentMethod == PaymentMethod.Online
                         && oi.Order.PaymentStatus == PaymentStatus.Pending
                         && DateTime.Now > oi.Order.OrderDate.AddMinutes(10))
                    .ExecuteUpdateAsync(prop => prop
                    .SetProperty(p => p.Status, OrderItemStatus.Cancelled)
                    .SetProperty(p => p.CancellationReason, "Payment timeout"), stoppingToken);


                    foreach (var order in orders)
                    {
                        await bus.Publish(new OrderCancelledEvent
                        {
                            OrderId = order.Id
                        }, stoppingToken);
                    }

                }
            }
        }
    }
}