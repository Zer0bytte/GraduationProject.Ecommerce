namespace Ecommerce.Application.Common.Configs;
public class HostingConfig
{
    public string HostName { get; set; } = default!;
}


public class SMTPConfig
{

    public string SenderEmail { get; set; } = default!;
    public string Host { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public bool UseSSl { get; set; } 
    public int Port { get; set; } = default!;


}

