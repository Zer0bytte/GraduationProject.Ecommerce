﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Orders.Commands.User.CancelOrderItem;
public class CancelOrderItemCommand : IRequest<CancelOrderItemResult>
{
    public Guid OrderItemId { get; set; }   
}