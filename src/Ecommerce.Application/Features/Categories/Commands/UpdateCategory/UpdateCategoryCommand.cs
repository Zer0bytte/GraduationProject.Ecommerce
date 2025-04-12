

namespace Ecommerce.Application.Features.Categories.Commands.UpdateCategory;
public record UpdateCategoryCommand : IRequest<UpdateCategoryResult>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}