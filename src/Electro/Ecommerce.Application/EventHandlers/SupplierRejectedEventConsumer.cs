using Ecommerce.Application.Templates;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Ecommerce.Application.EventHandlers;

public class SupplierRejectedEventConsumer(IEmailSender emailSender) : IConsumer<SupplierRejectedEvent>
{
    public async Task Consume(ConsumeContext<SupplierRejectedEvent> context)
    {

        var template = EmailTemplates.SupplierRejectedEmailTemplate;

        template = template.Replace("{SUPPLIER_NAME}", context.Message.SupplierName);
        template = template.Replace("{BUSINESS_NAME}", context.Message.BusinessName);
        template = template.Replace("{STORE_NAME}", context.Message.StoreName);
        template = template.Replace("{REVIEW_DATE}", DateTime.Now.ToShortDateString());
        template = template.Replace("{REJECTION_REASON}", context.Message.Reason);
        await emailSender.SendEmailAsync(context.Message.Email, "نأسف! تم رفض طلبك", template);
    }
}