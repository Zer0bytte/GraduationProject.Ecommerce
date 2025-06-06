namespace Ecommerce.Application.Features.Orders.Commands.User.MarkOrderAsCompleted;
public class MarkOrderAsCompletedCommand : IRequest<MarkOrderAsCompletedResult>
{
    public Guid OrderId { get; set; }
}