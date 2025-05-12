using Ecommerce.Application.Features.Conversations.Queries.GetConversations;
using Ecommerce.Application.Features.CouponCodes.Queries.GetCouponById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.CouponCodes.Queries.GetById;
public class GetCouponByIdQuery : IRequest<GetCouponByIdResult>
{
    public Guid Id { get; set; }
}