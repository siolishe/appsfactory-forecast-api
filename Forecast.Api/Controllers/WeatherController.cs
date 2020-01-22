using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IForecastService service,
            IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("forecast")]
        public async Task<Weather> GetForecastByCityName([FromQuery] string city,
            [FromQuery] string zipcode)
        {
            _logger.Log(LogLevel.Information, $"City = {city},Zipcode = {zipcode}");

            if (!string.IsNullOrWhiteSpace(city))
            {
                var response = await _service.GetWeatherByName(city);
                var result = _mapper.Map<WeatherService.CurrentWeather, Weather>(response);
                return result;
            }

            if (!string.IsNullOrWhiteSpace(zipcode))
            {
                var response = await _service.GetWeatherByZipCode(zipcode);
                var result = _mapper.Map<WeatherService.CurrentWeather, Weather>(response);
                return result;
            }

            return new Weather();
        }
    }
}