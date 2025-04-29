namespace Ecommerce.Application.Features.Suppliers.Commands.RejectSupplier;
public class RejectSupplierCommand : IRequest<RejectSupplierResult>
{
    public Guid SupplierId { get; set; }
    public string RejectionResult { get; set; } = default!;
}