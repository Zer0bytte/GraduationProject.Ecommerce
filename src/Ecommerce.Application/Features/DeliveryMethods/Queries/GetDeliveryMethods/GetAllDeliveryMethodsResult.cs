namespace Ecommerce.Application.Features.DeliveryMethods.Queries.GetDeliveryMethods;

public record GetAllDeliveryMethodsResult
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string DeliveryTime { get; set; } = default!;
    public decimal Price { get; set; }

}
