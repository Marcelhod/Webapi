using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

[ApiController]
[Route("api/weather")]
public class WeatherController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private const string ApiKey = "4fbb884b955be13c59272505c3217422";

    public WeatherController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet("{city}")]
    public async Task<IActionResult> GetWeather(string city)
    {
        var url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={ApiKey}";
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }

        return StatusCode((int)response.StatusCode, "Failed to fetch weather data.");
    }
}