using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Common.Models.Payment;
using Ecommerce.Application.Features.Payment;
using Newtonsoft.Json;

namespace Ecommerce.API.Endpoints.Payments;

public class PaymentEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/payment/webhook", async (HttpContext context, ISender sender, IClickPayService clickPayService) =>
        {
            string jsonPyload = await new StreamReader(context.Request.Body).ReadToEndAsync();
            PaymentWebhookPayload? payload = JsonConvert.DeserializeObject<PaymentWebhookPayload>(jsonPyload);

            Microsoft.Extensions.Primitives.StringValues signature = context.Request.Headers["Signature"];
            string hashed = clickPayService.ComputeHmacSha256(jsonPyload);
            if (hashed == signature)
            {
                await sender.Send(new PaymentWebhookCommand()
                {
                    Payload = payload!
                });
            }



            return Results.Ok();

        }).ExcludeFromDescription();
    }
}
