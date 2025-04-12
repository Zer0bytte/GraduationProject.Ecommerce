namespace BuildingBlocks.Exceptions;

public class EmailAlreadyExistsException() :
    InternalServerException("Email already in use. If you forgot your password, you can reset it.")
{
}



public class PhoneNumberAlreadyExistsException() :
    InternalServerException("Phone number already in use. If you forgot your password, you can reset it.")
{
}
