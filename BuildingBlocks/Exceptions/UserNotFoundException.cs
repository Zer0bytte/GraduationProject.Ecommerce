namespace BuildingBlocks.Exceptions;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(Guid id) : base($"User with Id: '{id}' not found!")
    {
    }
}
