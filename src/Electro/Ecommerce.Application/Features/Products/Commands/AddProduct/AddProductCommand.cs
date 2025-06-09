namespace Ecommerce.Application.Features.Products.Commands.AddProduct;

public record AddProductCommand : IRequest<AddProductResult>
{
    public string Title { get; set; } = default!;
    public decimal Price { get; set; }
    public int DiscountPercentage { get; set; }
    public int Stock { get; set; }
    public string SKU { get; set; } = default!;
    public List<string> Tags { get; set; } = default!;
    public string Brand { get; set; } = default!;
    public string Description { get; set; } = default!;
    public Guid CategoryId { get; set; }
    public List<IFormFile> Images { get; set; } = [];
    public bool IsAuction { get; set; }
    public DateTime? AuctionExpirationDate { get; set; }
    public decimal MinimumBidPrice { get; set; }
}
