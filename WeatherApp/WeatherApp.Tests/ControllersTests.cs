using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherApp.Api.Controllers;
using WeatherApp.Core.Interfaces;
using WeatherApp.Core.Models;

namespace WeatherApp.Tests;

public class ControllersTests
{
    private readonly Mock<ICountryService> _countryServiceMock;
    private readonly Mock<IWeatherService> _weatherServiceMock;
    private readonly CountriesController _countriesController;
    private readonly WeatherController _weatherController;

    public ControllersTests()
    {
        _countryServiceMock = new Mock<ICountryService>();
        _weatherServiceMock = new Mock<IWeatherService>();
        _countriesController = new CountriesController(_countryServiceMock.Object);
        _weatherController = new WeatherController(_weatherServiceMock.Object);
    }

    [Fact]
    public async Task GetCountries_ShouldReturnOkResult_WithCountries()
    {
        // Arrange
        var countries = new List<Country>
        {
            new() { Code = "US", Name = "United States" },
            new() { Code = "ID", Name = "Indonesia" }
        };

        _countryServiceMock.Setup(s => s.GetCountriesAsync()).ReturnsAsync(countries);

        // Act
        var result = await _countriesController.GetCountries();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedCountries = Assert.IsAssignableFrom<IEnumerable<Country>>(okResult.Value);

        Assert.Equal(2, returnedCountries.Count());
    }

    [Fact]
    public async Task GetCountries_ShouldReturnInternalServerError_WhenExceptionThrown()
    {
        // Arrange
        _countryServiceMock.Setup(s => s.GetCountriesAsync()).ThrowsAsync(new Exception("Service error"));

        // Act
        var result = await _countriesController.GetCountries();

        // Assert
        var statusResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(500, statusResult.StatusCode);
    }

    [Fact]
    public async Task GetCitiesByCountry_ShouldReturnOkResult_WithCities()
    {
        // Arrange
        var countryCode = "US";
        var cities = new List<City>
        {
            new() { Name = "New York", CountryCode = "US" },
            new() { Name = "Los Angeles", CountryCode = "US" }
        };

        _countryServiceMock.Setup(s => s.GetCitiesByCountryAsync(countryCode)).ReturnsAsync(cities);

        // Act
        var result = await _countriesController.GetCitiesByCountry(countryCode);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedCities = Assert.IsAssignableFrom<IEnumerable<City>>(okResult.Value);

        Assert.Equal(2, returnedCities.Count());
    }

    [Fact]
    public async Task GetCitiesByCountry_ShouldReturnBadRequest_WhenCountryCodeIsEmpty()
    {
        // Act
        var result = await _countriesController.GetCitiesByCountry("");

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(badRequestResult.Value);
    }

    [Fact]
    public async Task GetWeather_ShouldReturnOkResult_WithWeatherData()
    {
        // Arrange
        var cityName = "New York";
        var weatherResponse = new WeatherResponse
        {
            City = "New York",
            Country = "US",
            TemperatureFahrenheit = 68.0,
            TemperatureCelsius = 20.0,
            SkyConditions = "clear sky"
        };

        _weatherServiceMock.Setup(s => s.GetWeatherAsync(cityName)).ReturnsAsync(weatherResponse);

        // Act
        var result = await _weatherController.GetWeather(cityName);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedWeather = Assert.IsType<WeatherResponse>(okResult.Value);

        Assert.Equal("New York", returnedWeather.City);
        Assert.Equal(68.0, returnedWeather.TemperatureFahrenheit);
    }

    [Fact]
    public async Task GetWeather_ShouldReturnBadRequest_WhenCityNameIsEmpty()
    {
        // Act
        var result = await _weatherController.GetWeather("");

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

        Assert.NotNull(badRequestResult.Value);
    }

    [Fact]
    public async Task GetWeather_ShouldReturnServiceUnavailable_WhenHttpRequestExceptionThrown()
    {
        // Arrange
        var cityName = "InvalidCity";
        _weatherServiceMock.Setup(s => s.GetWeatherAsync(cityName))
            .ThrowsAsync(new HttpRequestException("API unavailable"));

        // Act
        var result = await _weatherController.GetWeather(cityName);

        // Assert
        var statusResult = Assert.IsType<ObjectResult>(result);

        Assert.Equal(503, statusResult.StatusCode);
    }

    [Fact]
    public async Task GetWeather_ShouldReturnInternalServerError_WhenArgumentNullExceptionThrown()
    {
        // Arrange
        var cityName = "TestCity";
        _weatherServiceMock.Setup(s => s.GetWeatherAsync(cityName))
            .ThrowsAsync(new ArgumentNullException("Configuration missing"));

        // Act
        var result = await _weatherController.GetWeather(cityName);

        // Assert
        var statusResult = Assert.IsType<ObjectResult>(result);

        Assert.Equal(500, statusResult.StatusCode);
    }
}