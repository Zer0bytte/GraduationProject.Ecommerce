namespace BuildingBlocks.Exceptions;

public class ExpiredCouponCodeException : InternalServerException
{
    public ExpiredCouponCodeException(string code) : base($"Coupon code: '{code}' expired")
    {
    }
}
