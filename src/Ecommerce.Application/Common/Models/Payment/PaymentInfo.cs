namespace Ecommerce.Application.Common.Models.Payment;

public class PaymentInfo
{
    [JsonProperty("card_type")]
    public string CardType { get; set; }

    [JsonProperty("card_scheme")]
    public string CardScheme { get; set; }

    [JsonProperty("payment_description")]
    public string PaymentDescription { get; set; }
}
