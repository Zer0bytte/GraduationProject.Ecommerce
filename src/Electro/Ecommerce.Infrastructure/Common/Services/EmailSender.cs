
using Ecommerce.Application.Common.Configs;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace Ecommerce.Infrastructure.Common.Services;
public class EmailSender(SMTPConfig sMTPConfig) : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        MimeMessage message = new MimeMessage();
        message.From.Add(new MailboxAddress("noreply", sMTPConfig.SenderEmail));
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
            await client.ConnectAsync(sMTPConfig.Host, sMTPConfig.Port, sMTPConfig.UseSSl);
            await client.AuthenticateAsync(sMTPConfig.Username, sMTPConfig.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
        }
    }
}