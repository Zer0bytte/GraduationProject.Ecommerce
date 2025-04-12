namespace BuildingBlocks.Exceptions.Abstractions;
public class DuplicateNamesException : Exception
{
    public DuplicateNamesException(string name) : base($"Entitiy with name: '{name}' already exists.")
    {

    }
}