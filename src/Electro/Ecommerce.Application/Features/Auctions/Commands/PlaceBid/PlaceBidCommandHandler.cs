
namespace Ecommerce.Application.Features.Auctions.Commands.PlaceBid;
public class PlaceBidCommandHandler(IApplicationDbContext context, ICurrentUser currentUser) : IRequestHandler<PlaceBidCommand, PlaceBidResult>
{
    public async Task<PlaceBidResult> Handle(PlaceBidCommand command, CancellationToken cancellationToken)
    {
        var product = await context.Products.Where(p => p.Id == command.ProductId).Select(p => new
        {
            p.Id,
            p.IsAuction,
            p.MinumumBidPrice,
            p.AuctionExpirationDate,
            MaximumBidPrice = p.AuctionBids.Max(b => b.Price),
            HaveSubmittedBid = p.AuctionBids.Any(b => b.UserId == currentUser.Id),
        }).FirstOrDefaultAsync(cancellationToken);

        if (product is null)
            throw new ApplicationException("لا يمكننا العثور علي هذا المنتج");

        if (!product.IsAuction.HasValue || !product.IsAuction.Value)
            throw new ApplicationException("عفوا, لا يمكنك تقديم عرض سعر علي منتج غير معروض");

        if (DateTime.Now > product.AuctionExpirationDate)
            throw new ApplicationException("عفوا, هذا المزاد انتهي");

        if (product.HaveSubmittedBid)
            throw new ApplicationException("لا يمكنك تقديم اكثر من عرض سعر");

        return new PlaceBidResult
        {

        };

    }
}