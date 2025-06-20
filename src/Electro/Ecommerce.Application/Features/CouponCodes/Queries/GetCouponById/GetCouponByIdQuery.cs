using Ecommerce.Application.Features.Conversations.Queries.GetConversations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.CouponCodes.Queries.GetCouponById;
public class GetCouponByIdQuery : IRequest<GetCouponByIdResult>
{
    public Guid Id { get; set; }
}