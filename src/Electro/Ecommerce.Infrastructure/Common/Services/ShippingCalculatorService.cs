using Ecommerce.Application.Common.Interfaces;

namespace Ecommerce.Infrastructure.Common.Services;
public static class CityCoordinates
{
    public static readonly Dictionary<string, (double Latitude, double Longitude)> Coordinates = new()
    {
        { "Cairo", (30.0444, 31.2357) },
        { "Giza", (29.9870, 31.2118) },
        { "Qalyubia", (30.1286, 31.2422) },
        { "Alexandria", (31.2001, 29.9187) },
        { "Beheira", (31.0341, 30.4682) },
        { "Matrouh", (31.3525, 27.2373) },
        { "Damietta", (31.4175, 31.8144) },
        { "Dakahlia", (31.0379, 31.3815) },
        { "Kafr El Sheikh", (31.1118, 30.9390) },
        { "Gharbia", (30.7865, 31.0019) },
        { "Menoufia", (30.5972, 30.9876) },
        { "Sharqia", (30.5877, 31.5020) },
        { "Port Said", (31.2653, 32.3019) },
        { "Ismailia", (30.5852, 32.2654) },
        { "Suez", (29.9668, 32.5498) },
        { "North Sinai", (31.1242, 33.8007) },
        { "South Sinai", (28.2415, 33.6167) },
        { "Beni Suef", (29.0744, 31.0978) },
        { "Fayoum", (29.3084, 30.8428) },
        { "Minya", (28.1099, 30.7503) },
        { "Asyut", (27.1801, 31.1893) },
        { "Sohag", (26.5499, 31.7000) },
        { "Qena", (26.1551, 32.7160) },
        { "Luxor", (25.6872, 32.6396) },
        { "Aswan", (24.0889, 32.8998) },
        { "Red Sea", (27.2579, 33.8116) },
    };

}
public class ShippingCalculatorService : IShippingCalculatorService
{
    private const decimal BaseFare = 20.0m; // Flat base
    private const decimal PerKmRate = 0.3m; // Rate per kilometer

    public decimal CalculateShippingPrice(string source, string dest)
    {

        if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(dest))
            return 0;
        if (!CityCoordinates.Coordinates.ContainsKey(source) || !CityCoordinates.Coordinates.ContainsKey(dest))
            return 0;

        var (lat1, lon1) = CityCoordinates.Coordinates[source];
        var (lat2, lon2) = CityCoordinates.Coordinates[dest];

        double distance = CalculateDistanceKm(lat1, lon1, lat2, lon2);
        decimal price = BaseFare + (decimal)distance * PerKmRate;


        return Math.Round(price, 2);
    }

    private double CalculateDistanceKm(double lat1, double lon1, double lat2, double lon2)
    {
        const double R = 6371; // Radius of Earth in KM
        double dLat = DegreesToRadians(lat2 - lat1);
        double dLon = DegreesToRadians(lon2 - lon1);

        double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        return R * c;
    }

    private double DegreesToRadians(double deg) => deg * (Math.PI / 180);
}
