namespace Ecommerce.Application.Features.Conversations.Queries.GetConversationMessages;
public class GetConversationMessageResult
{
    public MessageType MessageType { get; set; }
    public DateTime? ReadOn { get; set; }
    public DateTime SentOn { get; set; }
    public object Payload { get; set; }
    public bool IsIncoming { get; set; }
}