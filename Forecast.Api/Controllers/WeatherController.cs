using System.Threading.Tasks;
using Forecast.Domain.Services;
using Forecast.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppsFactory_WeatherForecast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IForecastService _service;

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IForecastService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("forecast")]
        public async Task<WeatherService.CurrentWeather> GetForecastByCityName([FromQuery] string city,
            [FromQuery] string zipcode)
        {
            _logger.Log(LogLevel.Information, $"City = {city},Zipcode = {zipcode}");

            if (!string.IsNullOrWhiteSpace(city))
            {
                var result = await _service.GetWeatherByName(city);
                return result;
            }

            if (!string.IsNullOrWhiteSpace(zipcode))
            {
                var result = await _service.GetWeatherByZipCode(zipcode);
                return result;
            }

            return new WeatherService.CurrentWeather();
        }
    }
}