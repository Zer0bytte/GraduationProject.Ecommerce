namespace Ecommerce.Application.Features.Charts.GetGlobalCharts;

public class GetGlobalChartsResult
{
    public List<UsersEachMonthResult> UsersReport { get; set; } = [];
    public int TotalUsersCount { get; set; }
    public int TotalSuppliersCount { get; set; }
    public int OrdersCount { get; set; }
    public int ProductsCount { get; set; }
}

public record UsersEachMonthResult
{
    public string Date { get; set; } = default!;
    public int RegisteredUsersCount { get; set; }
}