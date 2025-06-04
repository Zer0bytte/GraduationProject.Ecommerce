namespace Ecommerce.Application.Features.Payment;

public class PaymentWebhookCommandHandler(IApplicationDbContext context) : IRequestHandler<PaymentWebhookCommand>
{
    public async Task Handle(PaymentWebhookCommand command, CancellationToken cancellationToken)
    {
        Order? order = context.Orders.Find(Guid.Parse(command.Payload.CartId));
        if (order is null) return;

        List<OrderItemDetails> orderItems = context.OrderItems
            .Where(o => o.OrderId == order.Id)
            .Select(x => new OrderItemDetails
            {
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                Price = x.Price
            }).ToList();

        TransactionRefernce? transaction = context.TransactionRefernces.FirstOrDefault(t => t.TransactionRefId == command.Payload.TranRef);
        if (transaction is null) return;

        bool isPaymentApproved = command.Payload.PaymentResult.ResponseStatus == "A";

        if (isPaymentApproved)
        {
            order.PaymentStatus = PaymentStatus.Paid;
            transaction.PaidOn = DateTime.UtcNow;

            //foreach (OrderItemDetails item in orderItems)
            //{
            //    Product? product = await context.Products
            //        .Include(p => p.Supplier)
            //        .FirstOrDefaultAsync(p => p.Id == item.ProductId, cancellationToken);

            //    if (product?.Supplier is not null)
            //    {
            //        product.Supplier.Balance += item.Price * item.Quantity;
            //        product.Supplier.BalanceTransactions.Add(new SupplierBalanceTransaction
            //        {
            //            TransactionType = TransactionType.Revenue,
            //            Amount = item.Price * item.Quantity,
            //            Reason = $"Revenue from order: {order.Id}, and item: {product.Title}"
            //        });
            //    }
            //}
        }
        else
        {

            order.PaymentStatus = PaymentStatus.Failed;

            foreach (OrderItemDetails item in orderItems)
            {
                Product? product = context.Products.Find(item.ProductId);
                if (product is not null)
                {
                    product.Stock += item.Quantity;
                }
            }
        }

        await context.SaveChangesAsync(cancellationToken);


    }
}
public record OrderItemDetails
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}