namespace Ecommerce.Domain.Entities;
public class SupplierBalanceTransaction
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public TransactionType TransactionType { get; set; }
    public Guid SupplierId { get; set; }
    public SupplierProfile Supplier { get; set; }
    public string Reason { get; set; } = default!;
}

public enum TransactionType
{
    Withdraw,
    Revenue
}