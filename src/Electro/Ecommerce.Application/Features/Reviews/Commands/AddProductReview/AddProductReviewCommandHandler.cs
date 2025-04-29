namespace Ecommerce.Application.Features.Reviews.Commands.AddProductReview;

public class AddProductReviewCommandHandler(IApplicationDbContext context,
    ICurrentUser ICurrentUser, DirectoryConfiguration directoryConfiguration)
    : IRequestHandler<AddProductReviewCommand, AddProductReviewResult>
{
    public async Task<AddProductReviewResult> Handle(AddProductReviewCommand command, CancellationToken cancellationToken)
    {
        bool userAlreadyReviwed = await context.ProductReviews.AsNoTracking()
            .AnyAsync(rev => rev.ProductId == command.ProductId && rev.UserId == ICurrentUser.Id, cancellationToken);

        if (userAlreadyReviwed) throw new InternalServerException("You have already reviewed this product before!");

        bool productExist = await context.Products.AsNoTracking().AnyAsync(prd => prd.Id == command.ProductId);
        if (!productExist) throw new NotFoundException("Product", command.ProductId);

        bool userOrderedThisProduct = await context.Orders.AsNoTracking().AnyAsync(o => o.UserId == ICurrentUser.Id && o.OrderItems.Any(oi => oi.ProductId == command.ProductId));

        if (!userOrderedThisProduct) throw new InternalServerException("You can't review an item you havn't orderd!");

        ProductReview productReview = new ProductReview
        {
            ProductId = command.ProductId,
            Stars = command.Stars,
            UserId = ICurrentUser.Id,
            ReviewText = command.ReviewText,
        };

        if (command.Image is not null)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(command.Image.FileName);
            string filePath = Path.Combine(directoryConfiguration.MediaDirectory, fileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await command.Image.CopyToAsync(fileStream);
            }
            productReview.ReviewImageNameOnServer = fileName;
        }

        context.ProductReviews.Add(productReview);
        await context.SaveChangesAsync(cancellationToken);

        return new AddProductReviewResult
        {
            IsSuccess = true
        };
    }
}
