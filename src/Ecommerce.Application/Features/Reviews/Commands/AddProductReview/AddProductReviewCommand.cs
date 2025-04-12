namespace Ecommerce.Application.Features.Reviews.Commands.AddProductReview;

public class AddProductReviewCommand : IRequest<AddProductReviewResult>
{
    public Guid ProductId { get; set; }
    public int Stars { get; set; }
    public string ReviewText { get; set; } = default!;
    public IFormFile? Image { get; set; }
}
