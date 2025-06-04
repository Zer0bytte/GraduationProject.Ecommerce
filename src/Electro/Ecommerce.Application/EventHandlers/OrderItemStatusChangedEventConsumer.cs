using Ecommerce.Application.Templates;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Ecommerce.Application.EventHandlers;

public class OrderItemStatusChangedEventConsumer(IEmailSender emailSender) : IConsumer<OrderItemStatusChangedEvent>
{
    public async Task Consume(ConsumeContext<OrderItemStatusChangedEvent> context)
    {
        string template = EmailTemplates.OrderItemStatusChangedEmailTemplate;
        switch (context.Message.Status)
        {

            case OrderItemStatus.Confirmed:
                template = template.Replace("IMAGE_NAME", "order-confirmed.png");
                break;
            case OrderItemStatus.Shipped:
                template = template.Replace("IMAGE_NAME", "order-shipped.png");
                break;
            case OrderItemStatus.Delivered:
                template = template.Replace("IMAGE_NAME", "order-delivered.png");
                break;
            case OrderItemStatus.Cancelled:
                template = template.Replace("IMAGE_NAME", "order-shipped.png");
                break;
            default:
                break;
        }

        template = template.Replace("{CUSTOMER_NAME}", context.Message.CustomerName);
        template = template.Replace("{PRODUCT_NAME}", context.Message.ProductName);
        template = template.Replace("{DATE}", DateTime.UtcNow.ToShortDateString());
        await emailSender.SendEmailAsync(context.Message.Email, "تم تحديث حالة الطلب بنجاح", template);
    }
}
