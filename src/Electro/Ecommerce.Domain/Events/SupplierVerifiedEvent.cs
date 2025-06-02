namespace Ecommerce.Domain.Events;

public class SupplierVerifiedEvent
{
    public string Email { get; set; } = default!;
    public string StoreName { get; set; } = default!;
    public string BusinessName { get; set; } = default!;
    public string SupplierName { get; set; } = default!;

}
