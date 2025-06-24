using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using WeatherApp.Core.Models;
using WeatherApp.Infrastructure.Services;

namespace WeatherApp.Tests;

public class WeatherServiceTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly HttpClient _httpClient;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly WeatherService _weatherService;

    public WeatherServiceTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        _configurationMock = new Mock<IConfiguration>();
        
        _configurationMock.Setup(c => c["OpenWeatherMap:ApiKey"]).Returns("test-api-key");
        _weatherService = new WeatherService(_httpClient, _configurationMock.Object);
    }

    [Fact]
    public void ConvertFahrenheitToCelsius_ShouldConvertCorrectly()
    {
        // Arrange
        var fahrenheit = 32.0;
        var expectedCelsius = 0.0;

        // Act
        var result = _weatherService.ConvertFahrenheitToCelsius(fahrenheit);

        // Assert
        Assert.Equal(expectedCelsius, result);
    }

    [Fact]
    public void ConvertFahrenheitToCelsius_ShouldRoundToTwoDecimalPlaces()
    {
        // Arrange
        var fahrenheit = 98.6;
        var expectedCelsius = 37.0;

        // Act
        var result = _weatherService.ConvertFahrenheitToCelsius(fahrenheit);

        // Assert
        Assert.Equal(expectedCelsius, result);
    }

    [Fact]
    public void ConvertFahrenheitToCelsius_ShouldHandleNegativeTemperatures()
    {
        // Arrange
        var fahrenheit = -40.0;
        var expectedCelsius = -40.0;

        // Act
        var result = _weatherService.ConvertFahrenheitToCelsius(fahrenheit);

        // Assert
        Assert.Equal(expectedCelsius, result);
    }

    [Fact]
    public async Task GetWeatherAsync_ShouldReturnWeatherResponse_WhenApiCallSucceeds()
    {
        // Arrange
        var cityName = "New York";
        var mockResponse = new OpenWeatherMapResponse
        {
            Name = "New York",
            Sys = new Sys { Country = "US" },
            Dt = 1640995200, // Unix timestamp
            Main = new Main { Temp = 68.0, Humidity = 65, Pressure = 1013 },
            Wind = new Wind { Speed = 5.0, Deg = 180 },
            Visibility = 10000,
            Weather = new[] { new Weather { Description = "clear sky" } }
        };

        var jsonResponse = JsonSerializer.Serialize(mockResponse);
        var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(jsonResponse)
        };

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponse);

        // Act
        var result = await _weatherService.GetWeatherAsync(cityName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("New York", result.City);
        Assert.Equal("US", result.Country);
        Assert.Equal(68.0, result.TemperatureFahrenheit);
        Assert.Equal(20.0, result.TemperatureCelsius);
        Assert.Equal("clear sky", result.SkyConditions);
    }

    [Fact]
    public async Task GetWeatherAsync_ShouldThrowHttpRequestException_WhenApiCallFails()
    {
        // Arrange
        var cityName = "InvalidCity";
        var httpResponse = new HttpResponseMessage(HttpStatusCode.NotFound);

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponse);

        // Act & Assert
        await Assert.ThrowsAsync<HttpRequestException>(() => _weatherService.GetWeatherAsync(cityName));
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenApiKeyIsMissing()
    {
        // Arrange
        var configMock = new Mock<IConfiguration>();
        configMock.Setup(c => c["OpenWeatherMap:ApiKey"]).Returns((string?)null);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new WeatherService(_httpClient, configMock.Object));
    }
}