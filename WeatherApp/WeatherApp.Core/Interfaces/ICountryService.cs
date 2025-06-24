using WeatherApp.Core.Models;

namespace WeatherApp.Core.Interfaces;

public interface ICountryService
{
    Task<IEnumerable<Country>> GetCountriesAsync();

    Task<IEnumerable<City>> GetCitiesByCountryAsync(string countryCode);
}