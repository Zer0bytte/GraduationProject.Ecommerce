namespace Ecommerce.Application.Features.Categories.Queries.GetCategories;

public record GetCategoriesResult
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Image { get; set; }
}
