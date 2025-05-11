using BuildingBlocks.Exceptions.Abstractions;
using Ecommerce.Application.Common.Configs;
using Ecommerce.Application.Common.Interfaces;
using Ecommerce.Application.Common.Models.Payment;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace Ecommerce.Infrastructure.Common.Services;
public class ClickPayService(ClickPayConfig clickPayConfig) : IClickPayService
{
    public string ComputeHmacSha256(string jsonPayload)
    {
        using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(clickPayConfig.ServerKey)))
        {
            byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(jsonPayload));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }

    public async Task<ClickPayPaymentResponse> GeneratePaymentIntent(Guid orderId, decimal amount, string description)
    {
        ClickPayPaymentRequest paymentPayload = new ClickPayPaymentRequest
        {
            profile_id = clickPayConfig.ProfileId,
            tran_type = "sale",
            tran_class = "ecom",
            cart_description = description,
            cart_id = orderId.ToString(),
            cart_currency = "egp",
            cart_amount = amount,
            _return= "https://ecommerce.markomedhat.com/checkout-success"
        };

        using HttpClient http = new HttpClient();
        string paymentProviderUrl = "https://secure.clickpay.com.sa/payment/request";

        StringContent jsonContent = new StringContent(
                        JsonConvert.SerializeObject(paymentPayload),
                        Encoding.UTF8,
                        "application/json");
        http.DefaultRequestHeaders.Add("Authorization", clickPayConfig.ServerKey);
        HttpResponseMessage response = await http.PostAsync(paymentProviderUrl, jsonContent);

        if (response.IsSuccessStatusCode)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            ClickPayPaymentResponse? responseObject = JsonConvert.DeserializeObject<ClickPayPaymentResponse>(responseContent);
            return responseObject;
        }
        else
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            throw new InternalServerException(responseContent);
        }
    }
}