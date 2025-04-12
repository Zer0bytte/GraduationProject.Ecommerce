namespace Ecommerce.Application.Features.DeliveryMethods.Commands.AddDeliveryMethod;

public class AddDeliveryMethodCommandValidator : AbstractValidator<AddDeliveryMethodCommand>
{
    public AddDeliveryMethodCommandValidator()
    {
        RuleFor(dm => dm.ShortName).NotEmpty().WithMessage("Short name is required.");
        RuleFor(dm => dm.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(dm => dm.Price).GreaterThanOrEqualTo(1).WithMessage("Shipping price should be greater than '1' ");
        RuleFor(dm => dm.DeliveryTime).NotEmpty().WithMessage("Delivery time is required.");
    }
}
