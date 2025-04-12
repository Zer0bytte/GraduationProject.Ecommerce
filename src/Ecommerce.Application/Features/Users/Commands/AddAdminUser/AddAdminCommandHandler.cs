namespace Ecommerce.Application.Features.Users.Commands.AddAdminUser;

public class AddAdminCommandHandler(UserManager<AppUser> userManager) : IRequestHandler<AddAdminCommand, AddAdminResult>
{
    public async Task<AddAdminResult> Handle(AddAdminCommand command, CancellationToken cancellationToken)
    {
        if (await userManager.FindByEmailAsync(command.Email) != null) throw new EmailAlreadyExistsException();
        AppUser user = new AppUser
        {
            UserName = command.Email,
            Email = command.Email,
            FullName = command.FullName,
            PhoneNumber = command.PhoneNumber,
        };
        IdentityResult registerationResult = await userManager.CreateAsync(user);
        if (registerationResult.Succeeded)
        {
            string token = await userManager.GeneratePasswordResetTokenAsync(user);
            await userManager.AddToRoleAsync(user, "Admin");
            string confirmationCode = await userManager.GeneratePasswordResetTokenAsync(user);
            await SendEmail(user.Email, confirmationCode, user.Id);
        }

        return new AddAdminResult
        {
            UserId = user.Id,
        };
    }

    private async Task SendEmail(string email, string confirmationToken, Guid userId)
    {
        //IScheduler scheduler = await schedulerFactory.GetScheduler();

        //string jobId = $"email-job-{Guid.NewGuid()}";
        //string triggerId = $"email-trigger-{Guid.NewGuid()}";

        //IJobDetail job = JobBuilder.Create<SendEmailJob>()
        //    .WithIdentity(jobId)
        //    .UsingJobData("email", email)
        //    .UsingJobData("subject", "Set Your Password Now")
        //    .UsingJobData("details", $"To set your account password, please click the button below: {confirmationToken} and userId is: {userId}")
        //    .Build();

        //ITrigger trigger = TriggerBuilder.Create()
        //    .WithIdentity(triggerId)
        //    .StartNow()
        //    .Build();

        //await scheduler.ScheduleJob(job, trigger);
    }
}
