namespace Ecommerce.Application.Features.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand : IRequest<DeleteCategoryResult>
{
    public Guid CategoryId { get; set; }
}
