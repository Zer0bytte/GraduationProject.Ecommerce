namespace Ecommerce.Application.Features.Suppliers.Commands.VerifySupplier;

public class VerifySupplierCommand : IRequest<VerifySupplierResult>
{
    public Guid SupplierId { get; set; }

}
