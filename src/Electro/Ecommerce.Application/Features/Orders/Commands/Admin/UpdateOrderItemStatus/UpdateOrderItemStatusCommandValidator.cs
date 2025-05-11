namespace Ecommerce.Application.Features.Orders.Commands.Admin.UpdateOrderItemStatus;

public class UpdateOrderItemStatusCommandValidator : AbstractValidator<UpdateOrderItemStatusCommand>
{
    public UpdateOrderItemStatusCommandValidator()
    {
        RuleFor(x => x.OrderItemId).NotEmpty().WithMessage("معرف عنصر الطلب مطلوب");
        RuleFor(x => x.Status).IsInEnum().WithMessage("حالة عنصر الطلب غير صالحة");
    }
}