namespace Ecommerce.Application.Dtos;
public class ChatNotificationDto
{
    public Guid ConversationId { get; set; }
    public string MessageText { get; set; } = default!;
    public string SenderName { get; set; } = default!;

}

public class OrderNotificationDto
{
    public int ItemsCount { get; set; }
    public string BuyerName { get; set; } = default!;
}