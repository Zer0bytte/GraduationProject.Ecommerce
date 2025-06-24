using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Authentication.Commands.ChangeProfileDetails;
public class ChangeProfileDetailsCommandValidator : AbstractValidator<ChangeProfileDetailsCommand>
{
    public ChangeProfileDetailsCommandValidator()
    {
        RuleFor(user => user.PhoneNumber)
            .Matches(@"^[0-9]\d{9,14}$")
            .When(user => !string.IsNullOrWhiteSpace(user.PhoneNumber))
            .WithMessage("رقم الهاتف غير صالح");
    }
}