namespace Ecommerce.Application.Features.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteCategoryCommand, DeleteCategoryResult>
{
    public async Task<DeleteCategoryResult> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {
        Category? category = await context.Categories.FindAsync(command.CategoryId);
        if (category is null) throw new NotFoundException("Category", command.CategoryId);

        category.MarkAsDeleted();

        await context.SaveChangesAsync(cancellationToken);
        return new DeleteCategoryResult
        {
            IsSuccess = true
        };
    }
}
