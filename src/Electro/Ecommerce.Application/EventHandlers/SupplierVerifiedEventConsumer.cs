using Ecommerce.Application.Templates;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Ecommerce.Application.EventHandlers;

public class SupplierVerifiedEventConsumer(IEmailSender emailSender) : IConsumer<SupplierVerifiedEvent>
{
    public async Task Consume(ConsumeContext<SupplierVerifiedEvent> context)
    {

        var template = EmailTemplates.SupplierVerifiedEmailTemplate;

        template = template.Replace("{SUPPLIER_NAME}", context.Message.SupplierName);
        template = template.Replace("{BUSINESS_NAME}", context.Message.BusinessName);
        template = template.Replace("{STORE_NAME}", context.Message.StoreName);
        template = template.Replace("{VERIFICATION_DATE}", DateTime.Now.ToShortDateString());
        await emailSender.SendEmailAsync(context.Message.Email, "تم تأكيد متجرك بنجاح", template);
    }
}
