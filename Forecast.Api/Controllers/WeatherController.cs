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
        public async Task<Dto.Weather> GetWeather([FromQuery] string city,
            [FromQuery] string zipcode)
        {
            _logger.Log(LogLevel.Information, $"City = {city},Zipcode = {zipcode}");

            if (!string.IsNullOrWhiteSpace(city))
            {
                var response = await _service.GetWeatherByName(city);
                var result = _mapper.Map<WeatherService.CurrentWeather, Dto.Weather>(response);
                return result;
            }

            if (!string.IsNullOrWhiteSpace(zipcode))
            {
                var response = await _service.GetWeatherByZipCode(zipcode);
                var result = _mapper.Map<WeatherService.CurrentWeather, Dto.Weather>(response);
                return result;
            }

            return new Dto.Weather();
        }

        [HttpGet("forecast")]
        public async Task<IEnumerable<Dto.Forecast>> Get5DaysForecast([FromQuery] string city,
            [FromQuery] string zipcode)
        {
            _logger.Log(LogLevel.Information, $"City = {city},Zipcode = {zipcode}");

            if (!string.IsNullOrWhiteSpace(city))
            {
                var response = await _service.GetAllForecastByName(city);
                return response.list.Select(forecast =>
                    _mapper.Map<List, Dto.Forecast>(forecast)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(zipcode))
            {
                var response = await _service.GetAllForecastByZipCode(zipcode);
                return response.list.Select(forecast =>
                    _mapper.Map<List, Dto.Forecast>(forecast)).ToList();
            }

            return new List<Dto.Forecast>();
        }

        [HttpGet("forecastChart")]
        public async Task<IEnumerable<Dto.ForecastChart>> GetForecastChart([FromQuery] string city,
            [FromQuery] string zipcode)
        {
            _logger.Log(LogLevel.Information, $"City = {city},Zipcode = {zipcode}");

            if (!string.IsNullOrWhiteSpace(city))
            {
                var response = await _service.GetChartDataByName(city);
                return response.list.Select(forecast =>
                    _mapper.Map<List, Dto.ForecastChart>(forecast)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(zipcode))
            {
                var response = await _service.GetChartDataByZipCode(zipcode);
                return response.list.Select(forecast =>
                    _mapper.Map<List, Dto.ForecastChart>(forecast)).ToList();
            }

            return new List<Dto.ForecastChart>();
        }
    }
}