using Ecommerce.Application.Features.Orders.Queries.GetOrderPriceDetails;

namespace Ecommerce.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(IApplicationDbContext context, IClickPayService paymentService, IDistributedCache cache, ICurrentUser ICurrentUser, ISender sender)
    : IRequestHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        string? cartJson = await cache.GetStringAsync(command.CartId);
        if (string.IsNullOrEmpty(cartJson))
            throw new NotFoundException("Cart", command.CartId);

        var address = await context.Addresses.FirstOrDefaultAsync(ad => ad.Id == command.Address && ad.UserId == ICurrentUser.Id);
        if (address is null) throw new NotFoundException("Address", command.Address);

        CartDto? cart = JsonConvert.DeserializeObject<CartDto>(cartJson);
        List<OrderItem> orderItems = new();
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
        }


        var orderPriceDetails = await sender.Send(new GetOrderPriceDetailsQuery
        {
            AddressId = address.Id,
            CartId = command.CartId
        });
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
            SubTotal = orderPriceDetails.SubTotal,
            ShippingPrice = orderPriceDetails.ShippingPrice
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
                }
            }
        }

        string paymentUrl = string.Empty;
        if (command.PaymentMethod == PaymentMethod.Online)
        {
            order.PaymentStatus = PaymentStatus.Pending;
            Common.Models.Payment.ClickPayPaymentResponse paymentIntent = await paymentService.GeneratePaymentIntent(order.Id, orderTotalPrice, order.BuyerEmail);
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

        return new CreateOrderResult
        {
            IsSuccess = true,
            PaymentUrl = paymentUrl
        };
    }
}
