namespace Ecommerce.Application.Features.Address.Commands.DeleteAddress;
public record DeleteAddressCommand(Guid Id) : IRequest<DeleteAddressResult>;