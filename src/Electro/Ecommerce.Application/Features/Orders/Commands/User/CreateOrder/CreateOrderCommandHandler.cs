using Ecommerce.Application.Features.Orders.Queries.User.GetOrderPriceDetails;

namespace Ecommerce.Application.Features.Orders.Commands.User.CreateOrder;
public class AsyncLock
{
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

    public async Task<IDisposable> LockAsync()
    {
        await _semaphore.WaitAsync();
        return new Releaser(_semaphore);
    }

    private class Releaser : IDisposable
    {
        private readonly SemaphoreSlim _toRelease;
        public Releaser(SemaphoreSlim toRelease) => _toRelease = toRelease;
        public void Dispose() => _toRelease.Release();
    }
}
public class CreateOrderCommandHandler(IApplicationDbContext context, IClickPayService paymentService, IDistributedCache cache, ICurrentUser ICurrentUser, ISender sender)
    : IRequestHandler<CreateOrderCommand, CreateOrderResult>
{
    private static readonly AsyncLock _lock = new AsyncLock();

    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        using (await _lock.LockAsync())
        {
            string? cartJson = await cache.GetStringAsync(command.CartId);
            if (string.IsNullOrEmpty(cartJson))
                throw new NotFoundException("Cart", command.CartId);

            Domain.Entities.Address? address = await context.Addresses.FirstOrDefaultAsync(ad => ad.Id == command.Address && ad.UserId == ICurrentUser.Id);
            if (address is null) throw new NotFoundException("Address", command.Address);

            CartDto? cart = JsonConvert.DeserializeObject<CartDto>(cartJson);
            List<OrderItem> orderItems = new();
            decimal orderPriceDetails = 0;
            foreach (CartItemDto cartItem in cart.CartItems)
            {
                Product? product = await context.Products
                    .Include(p => p.Supplier)
                    .FirstOrDefaultAsync(p => p.Id == cartItem.Id, cancellationToken);

                if (product is null)
                    throw new NotFoundException("Product", cartItem.Id);

                if (product.Stock == 0)
                {
                    throw new LowStockException(product.Id);
                }

                product.Stock -= cartItem.Quantity;

                orderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Price = product.Price * (1 - product.Discount / 100m),
                    Quantity = cartItem.Quantity,
                    ProductName = product.Title,
                    ImageUrl = cartItem.ImageUrl,
                    SupplierId = product.SupplierId,
                    Status = OrderItemStatus.Pending
                });
                orderPriceDetails += product.Price * (1 - product.Discount / 100m);
            }

            Order order = new Order
            {
                Id = Guid.NewGuid(),
                BuyerEmail = ICurrentUser.Email,
                PaymentMethod = command.PaymentMethod,
                OrderDate = DateTime.UtcNow,
                OrderItems = orderItems,
                AddressId = command.Address,
                Status = OrderStatus.Pending,
                PaymentStatus = PaymentStatus.None,
                UserId = ICurrentUser.Id,
                SubTotal = orderPriceDetails,
            };

            decimal orderTotalPrice = order.SubTotal + order.ShippingPrice;

            if (!string.IsNullOrEmpty(command.CouponCode))
            {
                CouponCode? couponCode = await context.CouponCodes.FirstOrDefaultAsync(c => c.Code == command.CouponCode);
                if (couponCode is not null)
                {
                    if (DateTime.Now < couponCode.ExpirationDate)
                    {
                        decimal discountValue = couponCode.DiscountPercentage / 100 * orderTotalPrice;
                        if (discountValue > couponCode.MaximumDiscountValue)
                            discountValue = couponCode.MaximumDiscountValue;

                        orderTotalPrice = orderTotalPrice - discountValue;
                        order.SubTotal = orderTotalPrice;
                        order.CouponCode = command.CouponCode;
                    }
                }
            }

            string paymentUrl = string.Empty;
            if (command.PaymentMethod == PaymentMethod.Online)
            {
                order.PaymentStatus = PaymentStatus.Pending;
                Common.Models.Payment.ClickPayPaymentResponse paymentIntent = await paymentService.GeneratePaymentIntent(order.Id,
                    orderTotalPrice, order.BuyerEmail);
                paymentUrl = paymentIntent.RedirectUrl;


                TransactionRefernce transactionReference = new TransactionRefernce
                {
                    UserId = ICurrentUser.Id,
                    Amount = order.SubTotal,
                    OrderId = order.Id,
                    TransactionRefId = paymentIntent.TranRef,
                };
                await context.TransactionRefernces.AddAsync(transactionReference);
            }
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync(cancellationToken);
            await cache.RemoveAsync(command.CartId);
            return new CreateOrderResult
            {
                IsSuccess = true,
                PaymentUrl = paymentUrl
            };
        }
    }
}
