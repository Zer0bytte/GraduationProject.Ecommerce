namespace Ecommerce.Domain.Events;

public class SupplierRejectedEvent
{
    public string Email { get; set; } = default!;
    public string StoreName { get; set; } = default!;
    public string BusinessName { get; set; } = default!;
    public string SupplierName { get; set; } = default!;
    public string? Reason { get; set; }

}
