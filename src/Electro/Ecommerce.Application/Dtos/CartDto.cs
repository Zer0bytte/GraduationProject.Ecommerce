namespace Ecommerce.Application.Dtos;

public class CartDto
{
    public string Id { get; set; } = default!;
    public List<CartItemDto> CartItems { get; set; } = [];
    public int? DeliveryMethodId { get; set; }
    public decimal ShippingPrice { get; set; }
    public decimal SubTotal
    {
        get { return CartItems.Sum(i => i.Price * i.Quantity); }
    }

}
