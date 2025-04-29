namespace Ecommerce.Application.Features.Orders.Commands.UpdateOrderItemStatus;

public class UpdateOrderItemStatusCommandValidator : AbstractValidator<UpdateOrderItemStatusCommand>
{
    public UpdateOrderItemStatusCommandValidator()
    {
        RuleFor(x => x.OrderItemId).NotEmpty().WithMessage("Order item ID is required.");
        RuleFor(x => x.Status).IsInEnum().WithMessage("Invalid order item status.");
    }
} 