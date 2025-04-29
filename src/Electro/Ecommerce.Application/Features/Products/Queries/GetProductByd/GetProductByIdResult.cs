namespace Ecommerce.Application.Features.Products.Queries.GetProductByd;

public class OptionGroupDto
{
    public string OptionGroupName { get; set; } = default!;
    public List<OptionDto> Options { get; set; } = [];
}

public class OptionDto
{
    public string OptionName { get; set; } = default!;
    public decimal OptionPrice { get; set; }
}
public record GetProductByIdResult
{
    public Guid Id { get; set; }
    public Guid? SupplierId { get; set; }
    public string StoreName { get; set; }
    public string Title { get; set; } = default!;
    public decimal Price { get; set; }
    public decimal DiscountedPrice { get; set; }
    public int DiscountPercentage { get; set; }
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public int Stocks { get; set; }
    public string Tags { get; set; } = default!;
    public string SKU { get; set; } = default!;
    public string Brand { get; set; } = default!;
    public string[] Images { get; set; } = default!;
    public bool IsReviewd { get; set; }
    public List<ProductReviewResult> ProductReviews { get; set; } = [];
    public List<OptionGroupDto> ProductOptions { get; set; } = [];
}


public record ProductReviewResult
{
    public string? FullName { get; set; }
    public int Stars { get; set; }
    public string ReviewText { get; set; } = default!;
    public string? ReviewImage { get; set; }
}