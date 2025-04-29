namespace ShipX.Application.Common.Interfaces;
public interface IShippingCalculatorService
{
    decimal CalculateShippingPrice(string destination, string source);
}
