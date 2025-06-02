using Ecommerce.Application.Templates;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Ecommerce.Application.EventHandlers;

public class ResetPasswordCodeGeneratedEventConsumer(IEmailSender emailSender)
    : IConsumer<ResetPasswordCodeGeneratedEvent>
{
    public async Task Consume(ConsumeContext<ResetPasswordCodeGeneratedEvent> context)
    {
        string template = EmailTemplates.PasswordResetEmailTemplate;
        template = template.Replace("{RESET_CODE}", context.Message.ResetToken);
        await emailSender.SendEmailAsync(context.Message.Email, "استخدم هذا الرمز لإعادة تعيين كلمة مرورك", template);
    }
}
