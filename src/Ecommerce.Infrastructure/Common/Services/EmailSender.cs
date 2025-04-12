
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace Ecommerce.Infrastructure.Common.Services;
public class EmailSender : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        MimeMessage message = new MimeMessage();
        message.From.Add(new MailboxAddress("noreply", "noreply@zerobytetools.com"));
        message.To.Add(new MailboxAddress("zerobyte9955", email));
        message.Subject = subject;
        message.Body = new TextPart("html")
        {
            Text = htmlMessage
        };
        SmtpClient client = new SmtpClient();
        try
        {
            client.SslProtocols = System.Security.Authentication.SslProtocols.Tls12;
            await client.ConnectAsync("smtp.zeptomail.com", 587, false);
            await client.AuthenticateAsync("emailapikey", "wSsVR61yrB/yXPsrlGCudLsxmFlRVQ+nHE143lOl6HT9SKjAoMc/whDPAgb2GvROEzE8EjdD8LwqyxgCgGVc2tsvwllSDSiF9mqRe1U4J3x17qnvhDzDX2RdkRWAKoMAzgtjmmhhEM5u");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
        catch (Exception e)
        {
            Console.Write(e.Message);
        }
    }
}