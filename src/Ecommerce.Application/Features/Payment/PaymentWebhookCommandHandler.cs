namespace Ecommerce.Application.Features.Payment;

public class PaymentWebhookCommandHandler(IApplicationDbContext context) : IRequestHandler<PaymentWebhookCommand>
{
    public async Task Handle(PaymentWebhookCommand command, CancellationToken cancellationToken)
    {
        Order? order = context.Orders.Find(Guid.Parse(command.Payload.CartId));
        TransactionRefernce? transaction = context.TransactionRefernces.FirstOrDefault(t => t.TransactionRefId == command.Payload.TranRef);
        if (command.Payload.PaymentResult.ResponseStatus == "A")
        {
            if (transaction is not null)
            {
                transaction.Status = PaymentStatus.Paid;
                transaction.PaidOn = DateTime.UtcNow;
            }
            if (order is not null)
            {
                order.PaymentStatus = PaymentStatus.Paid;
            }
        }
        else
        {
            transaction.Status = PaymentStatus.Failed;
            order.PaymentStatus = PaymentStatus.Failed;
            transaction.PaidOn = DateTime.UtcNow;
            List<OrderItemDetails> orderItems = context.OrderItems.Where(o => o.OrderId == order.Id).Select(x => new OrderItemDetails { ProductId = x.ProductId, Quantity = x.Quantity }).ToList();
            foreach (OrderItemDetails? item in orderItems)
            {
                Product? product = context.Products.Find(item.ProductId);
                product.Stock += item.Quantity;
            }
        }

        await context.SaveChangesAsync(cancellationToken);

    }
}
public record OrderItemDetails
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}