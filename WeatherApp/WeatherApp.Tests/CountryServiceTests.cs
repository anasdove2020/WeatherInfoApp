using WeatherApp.Infrastructure.Services;

namespace WeatherApp.Tests;

public class CountryServiceTests
{
    private readonly CountryService _countryService;

    public CountryServiceTests()
    {
        _countryService = new CountryService();
    }

    [Fact]
    public async Task GetCountriesAsync_ShouldReturnAllCountries()
    {
        // Act
        var result = await _countryService.GetCountriesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Contains(result, c => c.Code == "US" && c.Name == "United States");
        Assert.Contains(result, c => c.Code == "ID" && c.Name == "Indonesia");
    }

    [Fact]
    public async Task GetCitiesByCountryAsync_ShouldReturnCitiesForValidCountry()
    {
        // Arrange
        var countryCode = "US";

        // Act
        var result = await _countryService.GetCitiesByCountryAsync(countryCode);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Contains(result, c => c.Name == "New York" && c.CountryCode == "US");
        Assert.Contains(result, c => c.Name == "Los Angeles" && c.CountryCode == "US");
    }

    [Fact]
    public async Task GetCitiesByCountryAsync_ShouldReturnEmptyForInvalidCountry()
    {
        // Arrange
        var countryCode = "INVALID";

        // Act
        var result = await _countryService.GetCitiesByCountryAsync(countryCode);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetCitiesByCountryAsync_ShouldBeCaseInsensitive()
    {
        // Arrange
        var countryCode = "us"; // lowercase

        // Act
        var result = await _countryService.GetCitiesByCountryAsync(countryCode);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Contains(result, c => c.Name == "New York" && c.CountryCode == "US");
    }

    [Fact]
    public async Task GetCitiesByCountryAsync_ShouldReturnIndonesianCities()
    {
        // Arrange
        var countryCode = "ID";

        // Act
        var result = await _countryService.GetCitiesByCountryAsync(countryCode);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Contains(result, c => c.Name == "Jakarta" && c.CountryCode == "ID");
        Assert.Contains(result, c => c.Name == "Surabaya" && c.CountryCode == "ID");
        Assert.Contains(result, c => c.Name == "Bandung" && c.CountryCode == "ID");
    }
}