namespace Ecommerce.Application.Features.Orders.Queries.GetOrderDetails;

public record OrderDetailsAddress
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public Governorate Governorate { get; set; }

}
