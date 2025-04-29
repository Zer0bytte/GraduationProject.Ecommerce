namespace Ecommerce.Application.Features.Products.Queries.GetProductByd;

public record GetProductByIdQuery : IRequest<GetProductByIdResult>
{
    public Guid Id { get; set; }
}
