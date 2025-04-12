namespace Ecommerce.Application.Features.DeliveryMethods.Commands.AddDeliveryMethod;

public record AddDeliveryMethodCommand : IRequest<AddDeliveryMethodResult>
{
    public string ShortName { get; set; } = default!;
    public string DeliveryTime { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
}
