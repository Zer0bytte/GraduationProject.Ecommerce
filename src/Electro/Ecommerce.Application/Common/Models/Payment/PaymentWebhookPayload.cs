namespace Ecommerce.Application.Common.Models.Payment;


public class PaymentWebhookPayload
{
    [JsonProperty("tran_ref")]
    public string TranRef { get; set; }

    [JsonProperty("cart_id")]
    public string CartId { get; set; }

    [JsonProperty("cart_description")]
    public string CartDescription { get; set; }

    [JsonProperty("cart_currency")]
    public string CartCurrency { get; set; }

    [JsonProperty("cart_amount")]
    public string CartAmount { get; set; }

    [JsonProperty("customer_details")]
    public CustomerDetails CustomerDetails { get; set; }

    [JsonProperty("payment_result")]
    public PaymentResult PaymentResult { get; set; }

    [JsonProperty("payment_info")]
    public PaymentInfo PaymentInfo { get; set; }

    [JsonProperty("token")]
    public string Token { get; set; }
}
