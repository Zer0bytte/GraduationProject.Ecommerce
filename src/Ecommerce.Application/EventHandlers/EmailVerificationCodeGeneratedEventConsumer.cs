using Microsoft.AspNetCore.Identity.UI.Services;

namespace Ecommerce.Application.EventHandlers;

public class EmailVerificationCodeGeneratedEventConsumer(IEmailSender emailSender) : IConsumer<EmailVerificationCodeGeneratedEvent>
{
    public async Task Consume(ConsumeContext<EmailVerificationCodeGeneratedEvent> context)
    {
        await emailSender.SendEmailAsync(context.Message.Email, "Electro Email Verification Code", context.Message.VerificationCode);
    }
}
