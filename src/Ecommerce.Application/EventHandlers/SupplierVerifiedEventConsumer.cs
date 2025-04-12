using Microsoft.AspNetCore.Identity.UI.Services;

namespace Ecommerce.Application.EventHandlers;

public class SupplierVerifiedEventConsumer(IEmailSender emailSender, IApplicationDbContext dbContext) : IConsumer<SupplierVerifiedEvent>
{
    public async Task Consume(ConsumeContext<SupplierVerifiedEvent> context)
    {
        SupplierProfile? supplier = await dbContext.SupplierProfiles.Include(s => s.User).FirstOrDefaultAsync();
        await emailSender.SendEmailAsync(supplier.User.Email, "Electro supplier profile accepted,", "Congratz");
    }
}
