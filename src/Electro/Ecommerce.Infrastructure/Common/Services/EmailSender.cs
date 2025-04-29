
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System.Reflection;

namespace Ecommerce.Infrastructure.Common.Services;
public class EmailSender : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        MimeMessage message = new MimeMessage();
        message.From.Add(new MailboxAddress("noreply", "noreply@markomedhat.com"));
        message.To.Add(new MailboxAddress("", email));
        message.Subject = subject;
        message.Body = new TextPart("html")
        {
            Text = htmlMessage
        };
        SmtpClient client = new SmtpClient();
        try
        {
            client.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            client.Connect("smtp.zeptomail.com", 587, false);
            client.Authenticate("emailapikey", "wSsVR61yr0OjX60sz2b8cr1rnF1WVVinEhh0jgGo7H+qSPzEpcc/l02YVAWkFPkYEWY9EzJDoL96mUsF02cGh9kpng0FDCiF9mqRe1U4J3x17qnvhDzOV2ldlRWJKIgMwQpomGZhFM8l+g==");
            client.Send(message);
            client.Disconnect(true);
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
        }
    }
}