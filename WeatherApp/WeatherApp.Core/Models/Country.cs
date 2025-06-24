namespace WeatherApp.Core.Models;

public class Country
{
    public string Code { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public List<City> Cities { get; set; } = [];
}