namespace BuildingBlocks.Exceptions;

public class EmailNotConfirmedException : InternalServerException
{
    public Guid UserId { get; set; }
    public EmailNotConfirmedException() : base("Your email is not confirmed, we have sent a confirmation code to your email address")
    {

    }
}
