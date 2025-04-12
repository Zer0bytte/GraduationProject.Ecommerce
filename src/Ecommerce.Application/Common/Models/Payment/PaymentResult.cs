namespace Ecommerce.Application.Common.Models.Payment;

public class PaymentResult
{
    [JsonProperty("response_status")]
    public string ResponseStatus { get; set; }

    [JsonProperty("response_code")]
    public string ResponseCode { get; set; }

    [JsonProperty("response_message")]
    public string ResponseMessage { get; set; }

    [JsonProperty("acquirer_message")]
    public string AcquirerMessage { get; set; }

    [JsonProperty("acquirer_rrn")]
    public string AcquirerRrn { get; set; }

    [JsonProperty("transaction_time")]
    public DateTime TransactionTime { get; set; }
}
