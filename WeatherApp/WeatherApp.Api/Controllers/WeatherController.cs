using Microsoft.AspNetCore.Mvc;
using WeatherApp.Core.Interfaces;

namespace WeatherApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController(IWeatherService weatherService) : ControllerBase
{
    private readonly IWeatherService _weatherService = weatherService;

    [HttpGet("{cityName}")]
    public async Task<IActionResult> GetWeather(string cityName)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(cityName))
            {
                return BadRequest(new { message = "City name is required" });
            }

            var weather = await _weatherService.GetWeatherAsync(cityName);
            return Ok(weather);
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(503, new { message = "Weather service is currently unavailable", error = ex.Message });
        }
        catch (ArgumentNullException ex)
        {
            return StatusCode(500, new { message = "Configuration error", error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while fetching weather data", error = ex.Message });
        }
    }
}