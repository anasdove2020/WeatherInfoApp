using WeatherApp.Core.Models;

namespace WeatherApp.Core.Interfaces;

public interface IWeatherService
{
    Task<WeatherResponse> GetWeatherAsync(string cityName);

    double ConvertFahrenheitToCelsius(double fahrenheit);
}