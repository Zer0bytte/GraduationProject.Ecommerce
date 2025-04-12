namespace Ecommerce.Application.Features.Address.Commands.DeleteAddress;
internal sealed class DeleteAddressCommandHandler(IApplicationDbContext context, ICurrentUser ICurrentUser) : IRequestHandler<DeleteAddressCommand, DeleteAddressResult>
{
    public async Task<DeleteAddressResult> Handle(DeleteAddressCommand command, CancellationToken cancellationToken)
    {
        Domain.Entities.Address? address = await context.Addresses.FindAsync(command.Id);

        if (address is null || address.UserId != ICurrentUser.Id)
            throw new NotFoundException("Address", command.Id);

        address.MarkAsDeleted();
        await context.SaveChangesAsync(cancellationToken);

        return new DeleteAddressResult
        {
            IsSuccess = true
        };
    }
}