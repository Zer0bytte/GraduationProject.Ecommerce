﻿namespace Ecommerce.Application.Features.Orders.Commands.User.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CartId).NotEmpty().WithMessage("معرف السلة مطلوب");

    }
}
