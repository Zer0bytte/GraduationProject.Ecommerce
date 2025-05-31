namespace Ecommerce.Application.Features.Suppliers.Queries.GetCurrentSupplier;
public class GetCurrentSupplierResult
{
    public Guid Id { get; set; }
    public string BusinessName { get; set; } = default!;
    public string StoreName { get; set; } = default!;
    public VerificationStatus VerificationStatus { get; set; }
    public decimal Balance { get; set; }
    public List<SalesChartItem> SalesChart { get; set; } = [];
}


public class SalesChartItem
{
    public string CategoryName { get; set; } = default!;
    public int Count { get; set; }
}