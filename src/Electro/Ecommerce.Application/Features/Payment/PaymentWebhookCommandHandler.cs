namespace Ecommerce.Application.Features.Payment;

public class PaymentWebhookCommandHandler(IApplicationDbContext context) : IRequestHandler<PaymentWebhookCommand>
{
    public async Task Handle(PaymentWebhookCommand command, CancellationToken cancellationToken)
    {
        Order? order = context.Orders.Find(Guid.Parse(command.Payload.CartId));
        if (order is null) return;

        var orderItems = await context.OrderItems.Where(o => o.OrderId == order.Id).ToListAsync();

        TransactionRefernce? transaction = context.TransactionRefernces.FirstOrDefault(t => t.TransactionRefId == command.Payload.TranRef);
        if (transaction is null) return;

        bool isPaymentApproved = command.Payload.PaymentResult.ResponseStatus == "A";

        if (isPaymentApproved)
        {
            order.PaymentStatus = PaymentStatus.Paid;
            transaction.PaidOn = DateTime.UtcNow;
        }
        else
        {

            order.PaymentStatus = PaymentStatus.Failed;
            order.CancellationReason = "Payment failed";
            foreach (OrderItem item in orderItems)
            {
                Product? product = context.Products.Find(item.ProductId);
                if (product is not null)
                {
                    product.Stock += item.Quantity;
                }
                item.Cancel("Payment failed!");


            }
        }

        await context.SaveChangesAsync(cancellationToken);


    }
}