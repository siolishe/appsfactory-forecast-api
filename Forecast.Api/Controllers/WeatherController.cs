using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("weather")]
        public async Task<Dto.Weather> GetWeather([FromQuery] string input)
        {
            _logger.Log(LogLevel.Information, $"Input = {input}");

            if (string.IsNullOrWhiteSpace(input))
                return new Dto.Weather();

            if (int.TryParse(input, out var zipcode))
            {
                var response = await _service.GetWeatherByZipCode(zipcode.ToString());
                var result = _mapper.Map<WeatherService.CurrentWeather, Dto.Weather>(response);
                return result;
            }
            else
            {
                var response = await _service.GetWeatherByName(input);
                var result = _mapper.Map<WeatherService.CurrentWeather, Dto.Weather>(response);
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
                var response = await _service.GetAllForecastByName(zipcode.ToString());
                return response.list.Select(forecast =>
                    _mapper.Map<List, Dto.Forecast>(forecast)).ToList();
            }
            else
            {
                var response = await _service.GetAllForecastByZipCode(input);
                return response.list.Select(forecast =>
                    _mapper.Map<List, Dto.Forecast>(forecast)).ToList();
            }
        }

        [HttpGet("forecastChart")]
        public async Task<IEnumerable<Dto.ForecastChart>> GetForecastChart([FromQuery] string input)
        {
            _logger.Log(LogLevel.Information, $"Input = {input}");

            if (string.IsNullOrWhiteSpace(input))
                return new List<Dto.ForecastChart>();

            if (int.TryParse(input, out var zipcode))
            {
                var response = await _service.GetChartDataByName(zipcode.ToString());
                return response.list.Select(forecast =>
                    _mapper.Map<List, Dto.ForecastChart>(forecast)).ToList();
            }
            else
            {
                var response = await _service.GetChartDataByZipCode(input);
                return response.list.Select(forecast =>
                    _mapper.Map<List, Dto.ForecastChart>(forecast)).ToList();
            }
        }
    }
}