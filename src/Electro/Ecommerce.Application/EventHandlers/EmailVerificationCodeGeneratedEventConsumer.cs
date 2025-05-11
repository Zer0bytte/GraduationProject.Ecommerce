using Ecommerce.Application.Templates;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Ecommerce.Application.EventHandlers;

public class EmailVerificationCodeGeneratedEventConsumer(IEmailSender emailSender) : IConsumer<EmailVerificationCodeGeneratedEvent>
{
    public async Task Consume(ConsumeContext<EmailVerificationCodeGeneratedEvent> context)
    {
        string template = EmailTemplates.EmailConfirmationTemplate;
        template = template.Replace("{UserName}", context.Message.Name);
        template = template.Replace("{CONFIRMATION_CODE}", context.Message.VerificationCode);
        await emailSender.SendEmailAsync(context.Message.Email, "Electro Email Verification Code", template);
    }
}
