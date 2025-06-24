namespace WeatherApp.Core.Models;

public class WeatherResponse
{
    public string City { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;

    public DateTime TimeUtc { get; set; }

    public WindInfo Wind { get; set; } = new();

    public double VisibilityMiles { get; set; }

    public string SkyConditions { get; set; } = string.Empty;

    public double TemperatureFahrenheit { get; set; }

    public double TemperatureCelsius { get; set; }

    public double DewPointFahrenheit { get; set; }

    public double DewPointCelsius { get; set; }

    public double RelativeHumidity { get; set; }

    public double PressureInHg { get; set; }
}

public class WindInfo
{
    public double SpeedMph { get; set; }

    public string Direction { get; set; } = string.Empty;
}