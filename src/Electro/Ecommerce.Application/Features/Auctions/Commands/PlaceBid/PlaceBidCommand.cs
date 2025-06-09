using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Auctions.Commands.PlaceBid;
public class PlaceBidCommand : IRequest<PlaceBidResult>
{
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
}