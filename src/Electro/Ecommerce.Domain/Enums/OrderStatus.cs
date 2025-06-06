using System.Runtime.Serialization;

namespace Ecommerce.Domain.Enums;

public enum OrderStatus
{
    [EnumMember(Value = "Pending")]
    Pending,
    [EnumMember(Value = "Confirmed")]
    Confirmed,
    [EnumMember(Value = "Shipped")]
    Shipped,
    [EnumMember(Value = "Delivered")]
    Delivered,
    [EnumMember(Value = "Cancelled")]
    Cancelled,
    Completed
}
