using Ecommerce.Application.Features.Orders.Commands.User.CreateOrder;

namespace Ecommerce.Application.Features.Orders.Commands.User.CancelOrderItem;
public class CancelOrderItemCommandHandler(ICurrentUser currentUser, IApplicationDbContext context) : IRequestHandler<CancelOrderItemCommand, CancelOrderItemResult>
{
    private static readonly AsyncLock _lock = new AsyncLock();

    public async Task<CancelOrderItemResult> Handle(CancelOrderItemCommand command, CancellationToken cancellationToken)
    {
        using (await _lock.LockAsync())
        {
            OrderItem? orderItem = await context.OrderItems
                .Include(oi => oi.Order)
                .ThenInclude(o => o.OrderItems)
                .FirstOrDefaultAsync(oi => oi.Id == command.OrderItemId && oi.Order.UserId == currentUser.Id, cancellationToken);
            if (orderItem is null) throw new NotFoundException("انت لا تمتلك طلب يحتوي علي هذا العنصر");

            if (orderItem.Status != OrderItemStatus.Pending)
            {
                throw new ApplicationException("عفوا, لا يمكنك الغاء طلب تم تأكيده بالفعل");
            }

            orderItem.Cancel();
            Product? product = await context.Products.FindAsync(orderItem.ProductId);

            if (product is null) throw new NotFoundException("لم يتم العثور علي هذا المنتج, برجاء التواصل مع الدعم");

            product.Stock += orderItem.Quantity;

            orderItem.Order.UpdateOrderStatusBasedOnItems();
            await context.SaveChangesAsync(cancellationToken);

            return new CancelOrderItemResult
            {
                IsSuccess = true
            };
        }
    }
}