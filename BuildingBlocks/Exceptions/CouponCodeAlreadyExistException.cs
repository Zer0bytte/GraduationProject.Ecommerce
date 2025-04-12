namespace BuildingBlocks.Exceptions;

public class CouponCodeAlreadyExistException : InternalServerException

{
    public CouponCodeAlreadyExistException(string code) : base($"Coupon code: '{code}' already exists!")
    {
    }
}
