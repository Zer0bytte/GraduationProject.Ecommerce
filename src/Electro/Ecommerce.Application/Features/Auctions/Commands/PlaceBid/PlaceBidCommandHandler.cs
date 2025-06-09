
namespace Ecommerce.Application.Features.Auctions.Commands.PlaceBid;
public class PlaceBidCommandHandler(IApplicationDbContext context, ICurrentUser currentUser) : IRequestHandler<PlaceBidCommand, PlaceBidResult>
{
    public async Task<PlaceBidResult> Handle(PlaceBidCommand command, CancellationToken cancellationToken)
    {
        var product = await context.Products.Where(p => p.Id == command.ProductId && p.IsAuction).Select(p => new
        {
            p.Id,
            MinumumPrice = p.AuctionBids.Any() ? p.AuctionBids.Max(b => b.Price) : p.MinumumBidPrice,
            p.AuctionExpirationDate,
            HaveSubmittedBid = p.AuctionBids.Any(b => b.UserId == currentUser.Id),
        }).FirstOrDefaultAsync(cancellationToken);

        if (product is null)
            throw new ApplicationException("لا يمكننا العثور علي هذا المنتج");



        if (DateTime.Now > product.AuctionExpirationDate)
            throw new ApplicationException("عفوا, هذا المزاد انتهي");

        if (product.HaveSubmittedBid)
            throw new ApplicationException("لا يمكنك تقديم اكثر من عرض سعر");


        if (command.Price <= product.MinumumPrice)
            throw new ApplicationException($"يمكنك فقط عرض سعر اعلي من: {product.MinumumPrice}");


        context.AuctionBids.Add(new AuctionBid
        {
            UserId = currentUser.Id,
            Price = command.Price,
            ProductId = command.ProductId,
        });
        await context.SaveChangesAsync(cancellationToken);

        return new PlaceBidResult
        {
            IsSuccess = true
        };

    }
}