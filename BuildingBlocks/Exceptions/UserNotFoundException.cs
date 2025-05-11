namespace BuildingBlocks.Exceptions;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException() : base($"لم يتم العثور علي هذا المستخدم")
    {
    }
}
