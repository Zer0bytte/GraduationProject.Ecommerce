using Ecommerce.Application.Common.Models.Payment;

namespace Ecommerce.Application.Features.Payment;

public class PaymentWebhookCommand : IRequest
{
    public PaymentWebhookPayload Payload { get; set; } = default!;
}
