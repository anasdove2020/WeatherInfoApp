using System.Text.Json;
using Microsoft.Extensions.Configuration;
using WeatherApp.Core.Interfaces;
using WeatherApp.Core.Models;

namespace WeatherApp.Infrastructure.Services;

public class WeatherService(HttpClient httpClient, IConfiguration configuration) : IWeatherService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly string _apiKey = configuration["OpenWeatherMap:ApiKey"] ?? throw new ArgumentNullException("OpenWeatherMap:ApiKey configuration is missing");
    private const string BaseUrl = "https://api.openweathermap.org/data/2.5";

    public async Task<WeatherResponse> GetWeatherAsync(string cityName)
    {
        try
        {
            var url = $"{BaseUrl}/weather?q={cityName}&appid={_apiKey}&units=imperial";
            var response = await _httpClient.GetAsync(url);
            
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Weather API request failed with status code: {response.StatusCode}");
            }

            var jsonContent = await response.Content.ReadAsStringAsync();
            var weatherData = JsonSerializer.Deserialize<OpenWeatherMapResponse>(jsonContent);

            return weatherData == null
                ? throw new InvalidOperationException("Failed to deserialize weather data")
                : MapToWeatherResponse(weatherData);
        }
        catch (HttpRequestException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error fetching weather data: {ex.Message}", ex);
        }
    }

    public double ConvertFahrenheitToCelsius(double fahrenheit)
    {
        return Math.Round((fahrenheit - 32) * 5.0 / 9.0, 2);
    }

    private WeatherResponse MapToWeatherResponse(OpenWeatherMapResponse data)
    {
        var temperatureF = data.Main.Temp;
        var dewPointF = data.Main.Temp - ((100 - data.Main.Humidity) / 5.0);
        
        return new WeatherResponse
        {
            City = data.Name,
            Country = data.Sys.Country,
            TimeUtc = DateTimeOffset.FromUnixTimeSeconds(data.Dt).UtcDateTime,
            Wind = new WindInfo
            {
                SpeedMph = Math.Round(data.Wind.Speed * 2.237, 2), // Convert m/s to mph
                Direction = GetWindDirection(data.Wind.Deg)
            },
            VisibilityMiles = Math.Round(data.Visibility * 0.000621371, 2), // Convert meters to miles
            SkyConditions = data.Weather.FirstOrDefault()?.Description ?? "Unknown",
            TemperatureFahrenheit = Math.Round(temperatureF, 2),
            TemperatureCelsius = ConvertFahrenheitToCelsius(temperatureF),
            DewPointFahrenheit = Math.Round(dewPointF, 2),
            DewPointCelsius = ConvertFahrenheitToCelsius(dewPointF),
            RelativeHumidity = data.Main.Humidity,
            PressureInHg = Math.Round(data.Main.Pressure * 0.02953, 2) // Convert hPa to inHg
        };
    }

    private static string GetWindDirection(int degrees)
    {
        var directions = new[] { "N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW" };
        var index = (int)Math.Round(degrees / 22.5) % 16;
        return directions[index];
    }
}