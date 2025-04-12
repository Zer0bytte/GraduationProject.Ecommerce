namespace Ecommerce.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CartId).NotEmpty().WithMessage("Cart it is required.");

    }
}
