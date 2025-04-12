namespace Ecommerce.Application.Features.Address.Queries.GetAddresses;

public class GetAddressesQueryHandler(IApplicationDbContext context, ICurrentUser ICurrentUser) : IRequestHandler<GetAddressesQuery, List<GetAddressesResult>>
{
    public async Task<List<GetAddressesResult>> Handle(GetAddressesQuery request, CancellationToken cancellationToken)
    {
        List<GetAddressesResult> addresses = await context.Addresses.Where(a => a.UserId == ICurrentUser.Id).Select(ad => new GetAddressesResult
        {
            Id = ad.Id,
            FirstName = ad.FirstName,
            LastName = ad.LastName,
            City = ad.City,
            Governorate = ad.Governorate,
            Street = ad.Street
        }).ToListAsync(cancellationToken);

        return addresses;
    }
}
