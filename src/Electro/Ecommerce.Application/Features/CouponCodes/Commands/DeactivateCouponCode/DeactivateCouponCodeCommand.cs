using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.CouponCodes.Commands.DeactivateCouponCode;
public class DeactivateCouponCodeCommand : IRequest<DeactivateCouponCodeResult>
{
    public Guid Id { get; set; }
}