using Ecommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Rules;
public static class OrderItemStatusTransitions
{
    private static readonly Dictionary<OrderItemStatus, OrderItemStatus[]> allowedTransitions = new()
    {
        { OrderItemStatus.Pending,   new[] { OrderItemStatus.Confirmed, OrderItemStatus.Shipped, OrderItemStatus.Cancelled } },
        { OrderItemStatus.Confirmed, new[] { OrderItemStatus.Shipped, OrderItemStatus.Cancelled } },
        { OrderItemStatus.Shipped,   new[] { OrderItemStatus.Delivered } },
        { OrderItemStatus.Delivered, Array.Empty<OrderItemStatus>() },
        { OrderItemStatus.Cancelled, Array.Empty<OrderItemStatus>() },
    };

    public static bool IsValidTransition(OrderItemStatus current, OrderItemStatus target)
    {
        return allowedTransitions.TryGetValue(current, out var nextStates) &&
               nextStates.Contains(target);
    }

    public static OrderItemStatus[] GetAllowedTransitions(OrderItemStatus current)
    {
        return allowedTransitions.TryGetValue(current, out var transitions) ? transitions : Array.Empty<OrderItemStatus>();
    }
}