namespace Ecommerce.Application.Features.DeliveryMethods.Queries.GetDeliveryMethods;

public class GetAllDeliveryMethodsQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAllDeliveryMethodsQuery, List<GetAllDeliveryMethodsResult>>
{
    public async Task<List<GetAllDeliveryMethodsResult>> Handle(GetAllDeliveryMethodsQuery request, CancellationToken cancellationToken)
    {
        List<GetAllDeliveryMethodsResult> deliveryMethods = await context.DeliveryMethods.Select(dm => new GetAllDeliveryMethodsResult
        {
            Id = dm.Id,
            Name = dm.ShortName,
            Description = dm.Description,
            Price = dm.Price,
            DeliveryTime = dm.DeliveryTime
        }).ToListAsync(cancellationToken);
        return deliveryMethods;
    }
}
