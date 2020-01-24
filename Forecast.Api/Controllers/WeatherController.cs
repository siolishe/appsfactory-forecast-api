using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppsFactory_WeatherForecast.Dto;
using AutoMapper;
using Forecast.Domain.Services;
using Forecast.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Weather = AppsFactory_WeatherForecast.Dto.Weather;

namespace AppsFactory_WeatherForecast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMapper _mapper;
        private readonly IForecastService _service;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IForecastService service,
            IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("weather")]
        public async Task<Weather> GetWeather([FromQuery] string input)
        {
            _logger.Log(LogLevel.Information, $"Input = {input}");

            if (string.IsNullOrWhiteSpace(input))
                return new Weather();

            if (int.TryParse(input, out var zipcode))
            {
                var response = await _service.GetWeatherByZipCode(zipcode.ToString());
                var result = _mapper.Map<WeatherService.CurrentWeather, Weather>(response);
                return result;
            }
            else
            {
                var response = await _service.GetWeatherByName(input);
                var result = _mapper.Map<WeatherService.CurrentWeather, Weather>(response);
                return result;
            }
        }

        [HttpGet("forecast")]
        public async Task<IEnumerable<Dto.Forecast>> Get5DaysForecast([FromQuery] string input)
        {
            _logger.Log(LogLevel.Information, $"Input = {input}");

            if (string.IsNullOrWhiteSpace(input))
                return new List<Dto.Forecast>();

            if (int.TryParse(input, out var zipcode))
            {
                var response = await _service.GetAllForecastByZipCode(zipcode.ToString());
                return response.list.Select(forecast =>
                    _mapper.Map<List, Dto.Forecast>(forecast)).ToList();
            }
            else
            {
                var response = await _service.GetAllForecastByName(input);
                return response.list.Select(forecast =>
                    _mapper.Map<List, Dto.Forecast>(forecast)).ToList();
            }
        }

        [HttpGet("forecastChart")]
        public async Task<IEnumerable<ForecastChart>> GetForecastChart([FromQuery] string input)
        {
            _logger.Log(LogLevel.Information, $"Input = {input}");

            if (string.IsNullOrWhiteSpace(input))
                return new List<ForecastChart>();

            if (int.TryParse(input, out var zipcode))
            {
                var response = await _service.GetChartDataByName(zipcode.ToString());
                return response.list.Select(forecast =>
                    _mapper.Map<List, ForecastChart>(forecast)).ToList();
            }
            else
            {
                var response = await _service.GetChartDataByName(input);
                return response.list.Select(forecast =>
                    _mapper.Map<List, ForecastChart>(forecast)).ToList();
            }
        }
    }
}