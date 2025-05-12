using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.CouponCodes.Commands.UpdateCouponCode;
public record UpdateCouponCodeResult
{
    public bool IsSuccess { get; set; }
}