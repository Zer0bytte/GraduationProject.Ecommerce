namespace Ecommerce.Application.Common.Models.Payment;

public class ClickPayPaymentResponse
{
    [JsonProperty("tran_ref")]
    public string TranRef { get; set; } // Transaction reference

    [JsonProperty("tran_type")]
    public string TranType { get; set; } // Transaction type (e.g., Sale)

    [JsonProperty("cart_id")]
    public string CartId { get; set; } // Cart ID

    [JsonProperty("cart_description")]
    public string CartDescription { get; set; } // Description of the items/services

    [JsonProperty("cart_currency")]
    public string CartCurrency { get; set; } // Currency of the cart (e.g., EGP)

    [JsonProperty("cart_amount")]
    public string CartAmount { get; set; } // Amount of the cart

    [JsonProperty("tran_total")]
    public string TranTotal { get; set; } // Total transaction amount

    [JsonProperty("callback")]
    public string Callback { get; set; } // Callback URL for webhook

    [JsonProperty("return")]
    public string Return { get; set; } // Return URL for the frontend

    [JsonProperty("redirect_url")]
    public string RedirectUrl { get; set; } // Redirect URL for the payment gateway

    [JsonProperty("serviceId")]
    public int ServiceId { get; set; } // Service ID

    [JsonProperty("profileId")]
    public int ProfileId { get; set; } // Profile ID

    [JsonProperty("merchantId")]
    public int MerchantId { get; set; } // Merchant ID

    [JsonProperty("trace")]
    public string Trace { get; set; } // Trace ID for debugging
}