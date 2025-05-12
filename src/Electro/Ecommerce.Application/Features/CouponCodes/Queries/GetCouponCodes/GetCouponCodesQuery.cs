using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.CouponCodes.Queries.GetCouponCodes;
public class GetCouponCodesQuery : PagedQuery, IRequest<PagedResult<GetCouponCodesResult>>
{
    public string? SearchQuery { get; set; }

}