namespace Ecommerce.Application.Features.Suppliers.Queries.GetAllSuppliers;

public class GetAllSuppliersQueryHandler(IApplicationDbContext context) : IRequestHandler<GetAllSuppliersQuery, PagedResult<GetAllSuppliersResult>>
{
    public async Task<PagedResult<GetAllSuppliersResult>> Handle(GetAllSuppliersQuery query, CancellationToken cancellationToken)
    {
        IQueryable<SupplierProfile> baseQuery = context.SupplierProfiles.AsQueryable();


        baseQuery = baseQuery.Where(s => s.IsVerified == false);


        IQueryable<GetAllSuppliersResult> source = baseQuery.Select(sup => new GetAllSuppliersResult
        {
            SupplierId = sup.Id,
            BusinessName = sup.BusinessName,
            FullName = sup.User.FullName,
            StoreName = sup.StoreName,
            IsVerified = sup.IsVerified
        });



        IQueryable<GetAllSuppliersResult> suppliers = source
           .Skip((query.Page - 1) * query.Limit)
           .Take(query.Limit);

        int total = await source.CountAsync(cancellationToken: cancellationToken);
        return PagedResult<GetAllSuppliersResult>.Create(query, total, suppliers);

    }
}
