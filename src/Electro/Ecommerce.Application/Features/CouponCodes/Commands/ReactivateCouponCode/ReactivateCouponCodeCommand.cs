using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.CouponCodes.Commands.ReactivateCouponCode;
public class ReactivateCouponCodeCommand : IRequest<ReactivateCouponCodeResult>
{
    public Guid Id { get; set; }
}