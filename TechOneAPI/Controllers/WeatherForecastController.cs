using Microsoft.AspNetCore.Mvc;

namespace TechOneAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : Controller
    {

        // Declaring a private field for HttpClient and ApiKey
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        // Constructor to initialize HttpClient and to get API key from configuration
        public WeatherController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration.GetValue<string>("WeatherApi:OpenWeatherApiKey");
        }

        // HTTP GET method to fetch weather data
        [HttpGet]
        public async Task<IActionResult> GetWeather([FromQuery] string city)
        {
            // Constructing the API request URL with the city and API key
            var response = await _httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric");

            // Checking if the response is successful
            if (response.IsSuccessStatusCode)
            {
                // Reading the response content as a string
                var data = await response.Content.ReadAsStringAsync();
                // Returning the data with an OK (200) status
                return Ok(data);
            }
            // Returning a BadRequest (400) status if the response is not successful
            return BadRequest();
        }
    }
}
