using Ecommerce.Application.Templates;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Ecommerce.Application.EventHandlers;


public class AdminUserCreatedEventHandler(IEmailSender emailSender) : IConsumer<AdminUserCreatedEvent>
{
    public async Task Consume(ConsumeContext<AdminUserCreatedEvent> context)
    {
        var url = $"https://electroo.vercel.app/admin/set-password?userId={context.Message.Id}&token={context.Message.SetPasswordToken}";
        await emailSender.SendEmailAsync(context.Message.Email,
            "ضع كلمة مرور الان", EmailTemplates.SetAdminPasswordEmail.Replace("{SET_URL}", url));
    }
}