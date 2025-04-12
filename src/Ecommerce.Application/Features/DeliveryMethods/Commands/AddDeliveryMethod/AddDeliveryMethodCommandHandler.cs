namespace Ecommerce.Application.Features.DeliveryMethods.Commands.AddDeliveryMethod;

public class AddDeliveryMethodCommandHandler(IApplicationDbContext context) : IRequestHandler<AddDeliveryMethodCommand, AddDeliveryMethodResult>
{
    public async Task<AddDeliveryMethodResult> Handle(AddDeliveryMethodCommand command, CancellationToken cancellationToken)
    {
        if (context.DeliveryMethods.Any(dm => dm.ShortName == command.ShortName))
            throw new DuplicateNamesException(command.ShortName);


        DeliveryMethod dm = new DeliveryMethod
        {
            Description = command.Description,
            ShortName = command.ShortName,
            Price = command.Price,
            DeliveryTime = command.DeliveryTime
        };

        context.DeliveryMethods.Add(dm);
        await context.SaveChangesAsync(cancellationToken);

        return new AddDeliveryMethodResult
        {
            IsSuccess = true
        };
    }
}
