using WeatherApp.Core.Interfaces;
using WeatherApp.Core.Models;

namespace WeatherApp.Infrastructure.Services;

public class CountryService : ICountryService
{
    private readonly List<Country> _countries;

    public CountryService()
    {
        _countries = SeedCountriesData();
    }

    public Task<IEnumerable<Country>> GetCountriesAsync()
    {
        return Task.FromResult(_countries.AsEnumerable());
    }

    public Task<IEnumerable<City>> GetCitiesByCountryAsync(string countryCode)
    {
        var country = _countries.FirstOrDefault(c => c.Code.Equals(countryCode, StringComparison.OrdinalIgnoreCase));
        var cities = country?.Cities ?? [];
        return Task.FromResult(cities.AsEnumerable());
    }

    private static List<Country> SeedCountriesData()
    {
        return
        [
            new() {
                Code = "US",
                Name = "United States",
                Cities =
                [
                    new() { Name = "New York", CountryCode = "US" },
                    new() { Name = "Los Angeles", CountryCode = "US" },
                    new() { Name = "Chicago", CountryCode = "US" },
                    new() { Name = "Philadelphia", CountryCode = "US" },
                    new() { Name = "San Diego", CountryCode = "US" }
                ]
            },
            new Country
            {
                Code = "GB",
                Name = "United Kingdom",
                Cities =
                [
                    new() { Name = "London", CountryCode = "GB" },
                    new() { Name = "Manchester", CountryCode = "GB" },
                    new() { Name = "Birmingham", CountryCode = "GB" },
                    new() { Name = "Liverpool", CountryCode = "GB" },
                    new() { Name = "Edinburgh", CountryCode = "GB" }
                ]
            },
            new Country
            {
                Code = "ID",
                Name = "Indonesia",
                Cities =
                [
                    new() { Name = "Jakarta", CountryCode = "ID" },
                    new() { Name = "Surabaya", CountryCode = "ID" },
                    new() { Name = "Bandung", CountryCode = "ID" },
                    new() { Name = "Medan", CountryCode = "ID" },
                    new() { Name = "Semarang", CountryCode = "ID" },
                    new() { Name = "Palembang", CountryCode = "ID" },
                    new() { Name = "Makassar", CountryCode = "ID" },
                    new() { Name = "Yogyakarta", CountryCode = "ID" }
                ]
            }
        ];
    }
}