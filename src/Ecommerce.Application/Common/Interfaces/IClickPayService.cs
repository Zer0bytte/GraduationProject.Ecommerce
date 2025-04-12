using Ecommerce.Application.Common.Models.Payment;

namespace Ecommerce.Application.Common.Interfaces;
public interface IClickPayService
{
    string ComputeHmacSha256(string jsonPayload);
    Task<ClickPayPaymentResponse> GeneratePaymentIntent(Guid orderId, decimal amount, string description);
}
