using Ecommerce.Application.Templates;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Reflection;
using System.Xml.Linq;

namespace Ecommerce.Application.EventHandlers;

public class EmailVerificationCodeGeneratedEventConsumer(IEmailSender emailSender) : IConsumer<EmailVerificationCodeGeneratedEvent>
{
    public async Task Consume(ConsumeContext<EmailVerificationCodeGeneratedEvent> context)
    {
        var template = EmailTemplates.WelcomeEmail;
        template = template.Replace("{UserName}", context.Message.Name);
        template = template.Replace("{VerificationCode}", context.Message.VerificationCode);
        await emailSender.SendEmailAsync(context.Message.Email, "Electro Email Verification Code", template);
    }
}
