namespace Ecommerce.Application.Common.Configs;

public class RateLimitConfiguration
{
    public bool Enabled { get; set; }
    public int PermitLimit { get; set; } = 5;
    public int WindowInSeconds { get; set; } = 10;
    public int QueueLimit { get; set; } = 0;
    public string QueueProcessingOrder { get; set; } = "OldestFirst";
}
