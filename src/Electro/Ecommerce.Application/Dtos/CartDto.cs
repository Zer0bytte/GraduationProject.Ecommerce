namespace Ecommerce.Application.Dtos;

public class CartDto
{
    public string Id { get; set; } = default!;
    public List<CartItemDto> CartItems { get; set; } = [];
    public int? DeliveryMethodId { get; set; }
    public decimal ShippingPrice { get; set; }
    public decimal SubTotal
    {
        get { return GetSubTotal(); }
    }

    private decimal GetSubTotal()
    {
        decimal subtotal = 0;
        foreach (var item in CartItems)
        {
            if (item.DiscountedPrice > 0)
            {
                subtotal += item.DiscountedPrice * item.Quantity;
            }
            else
            {
                subtotal += item.Price * item.Quantity;
            }

        }

        return subtotal;
    }
}
