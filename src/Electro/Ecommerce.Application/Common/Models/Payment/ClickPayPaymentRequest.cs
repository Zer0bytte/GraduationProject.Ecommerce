namespace Ecommerce.Application.Common.Models.Payment;

public class ClickPayPaymentRequest
{
    public int profile_id { get; set; }
    public string tran_type { get; set; } = default!;
    public string tran_class { get; set; } = default!;
    public string cart_description { get; set; } = default!;
    public string cart_id { get; set; } = default!;
    public string cart_currency { get; set; } = default!;
    public decimal cart_amount { get; set; }
    public string callback { get; set; } = default!;
    [JsonProperty("return")]
    public string _return { get; set; } = default!;
}
