namespace Ecommerce.Application.Common.Interfaces;
public interface IShippingCalculatorService
{
    decimal CalculateShippingPrice(string destination, string source);
}
