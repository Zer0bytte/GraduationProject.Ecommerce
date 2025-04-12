namespace Ecommerce.Application.Features.Address.Commands.UpdateAddress;

public class UpdateAddressCommandHandler(IApplicationDbContext context, ICurrentUser ICurrentUser) : IRequestHandler<UpdateAddressCommand, UpdateAddressResult>
{
    public async Task<UpdateAddressResult> Handle(UpdateAddressCommand command, CancellationToken cancellationToken)
    {
        Domain.Entities.Address? address = await context.Addresses.FindAsync(command.Id);

        if (address is null || address.UserId != ICurrentUser.Id)
            throw new NotFoundException("Address", command.Id);

        address.FirstName = command.FirstName;
        address.LastName = command.LastName;
        address.PhoneNumber = command.PhoneNumber;
        address.City = command.City;
        address.Governorate = command.Governorate;
        address.Street = command.Street;

        await context.SaveChangesAsync(cancellationToken);
        return new UpdateAddressResult
        {
            IsSuccess = true
        };
    }
}
