namespace Ecommerce.Application.Features.Address.Commands.AddAddress;

public class AddAddressCommandHandler(IApplicationDbContext context, ICurrentUser ICurrentUser)
    : IRequestHandler<AddAddressCommand, AddAddressResult>
{
    public async Task<AddAddressResult> Handle(AddAddressCommand command, CancellationToken cancellationToken)
    {
        Domain.Entities.Address address = new Domain.Entities.Address
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            City = command.City,
            Governorate = command.Governorate,
            Street = command.Street,
            PhoneNumber = command.PhoneNumber,
            UserId = ICurrentUser.Id
        };
        context.Addresses.Add(address);
        await context.SaveChangesAsync(cancellationToken);
        return new AddAddressResult
        {
            IsSuccess = true
        };
    }
}
