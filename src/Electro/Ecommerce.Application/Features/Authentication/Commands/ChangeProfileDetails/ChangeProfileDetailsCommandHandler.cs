namespace Ecommerce.Application.Features.Authentication.Commands.ChangeProfileDetails;
public class ChangeProfileDetailsCommandHandler(IApplicationDbContext context, ICurrentUser currentUser) : IRequestHandler<ChangeProfileDetailsCommand, ChangeProfileDetailsResult>
{
    public async Task<ChangeProfileDetailsResult> Handle(ChangeProfileDetailsCommand command, CancellationToken cancellationToken)
    {
        AppUser? user = await context.Users.FindAsync(currentUser.Id);
        if (user is null) throw new NotFoundException("لا يمكننا العثور علي هذا المستخدم");

        if (!string.IsNullOrWhiteSpace(command.FullName))
        {
            user.FullName = command.FullName;
        }

        if (!string.IsNullOrWhiteSpace(command.PhoneNumber))
        {
            if (context.Users.Any(u => u.PhoneNumber == command.PhoneNumber && u.Id != currentUser.Id))
            {
                throw new Exceptions.ApplicationException("هذا الرقم مستخدم بالفعل");
            }
            user.FullName = command.PhoneNumber;
        }

        await context.SaveChangesAsync(cancellationToken);

        return new ChangeProfileDetailsResult
        {
            IsSuccess = true
        };
    }
}