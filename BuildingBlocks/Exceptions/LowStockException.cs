namespace BuildingBlocks.Exceptions;

public class LowStockException(Guid id) : BadRequestException($"Item with id: {id} is out of stock")
{
}
