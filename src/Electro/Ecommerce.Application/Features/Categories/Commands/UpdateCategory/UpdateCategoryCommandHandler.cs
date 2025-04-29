namespace Ecommerce.Application.Features.Categories.Commands.UpdateCategory;
internal sealed class UpdateCategoryCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateCategoryCommand, UpdateCategoryResult>
{
    public async Task<UpdateCategoryResult> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        Category? category = await context.Categories.FindAsync(command.Id);
        if (category is null)
            throw new NotFoundException("Category", command.Id);

        bool categoryExist = context.Categories.Any(x => x.Name == command.Name && x.Id != command.Id);
        if (categoryExist) throw new DuplicateNamesException(command.Name);

        category.Name = command.Name;

        category.ModifiedOn = DateTime.UtcNow;

        await context.SaveChangesAsync(cancellationToken);

        return new UpdateCategoryResult
        {
            IsSuccess = true
        };
    }
}