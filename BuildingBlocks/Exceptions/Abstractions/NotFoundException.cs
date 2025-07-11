﻿namespace BuildingBlocks.Exceptions.Abstractions;
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {

    }

    public NotFoundException(string name, object key) :
        base($"Entitiy {name} ({key}) was not found")
    {

    }
}
