namespace Ecommerce.Application.Features.Suppliers.Queries.GetSupplierById;

public record GetSupplierByIdQuery : IRequest<GetSupplierByIdResult>
{
    public Guid Id { get; set; }
}
