
namespace Ecommerce.Application.Features.Categories.Commands.AddCategory;

public class AddCategoryCommand : IRequest<AddCategoryResult>
{
    public string Name { get; set; } = default!;
    public IFormFile Image { get; set; } = default!;
}
