using Microsoft.AspNetCore.Mvc;
using WeatherApp.Core.Interfaces;

namespace WeatherApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController(ICountryService countryService) : ControllerBase
{
    private readonly ICountryService _countryService = countryService;

    [HttpGet]
    public async Task<IActionResult> GetCountries()
    {
        try
        {
            var countries = await _countryService.GetCountriesAsync();
            return Ok(countries);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while fetching countries", error = ex.Message });
        }
    }

    [HttpGet("{countryCode}/cities")]
    public async Task<IActionResult> GetCitiesByCountry(string countryCode)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(countryCode))
            {
                return BadRequest(new { message = "Country code is required" });
            }

            var cities = await _countryService.GetCitiesByCountryAsync(countryCode);
            return Ok(cities);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while fetching cities", error = ex.Message });
        }
    }
}