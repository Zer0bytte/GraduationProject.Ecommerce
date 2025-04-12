namespace Ecommerce.Application.Dtos;

public class CartItemDto
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public string Title { get; set; } = default!;
    public decimal Price { get; set; }
    public decimal DiscountedPrice { get; set; }
    public int DiscountPercentage { get; set; }
    public string Category { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
}
