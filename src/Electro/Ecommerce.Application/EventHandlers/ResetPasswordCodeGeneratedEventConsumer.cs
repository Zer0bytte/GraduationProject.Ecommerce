using Microsoft.AspNetCore.Identity.UI.Services;

namespace Ecommerce.Application.EventHandlers;

public class ResetPasswordCodeGeneratedEventConsumer(IEmailSender emailSender, IApplicationDbContext dbContext) 
    : IConsumer<ResetPasswordCodeGeneratedEvent>
{
    public async Task Consume(ConsumeContext<ResetPasswordCodeGeneratedEvent> context)
    {
        await emailSender.SendEmailAsync(context.Message.Email, "استخدم هذا الرمز لإعادة تعيين كلمة مرورك",context.Message.ResetToken);
    }
}
