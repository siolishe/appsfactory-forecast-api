using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Forecast.Domain.Repositories;
using Forecast.Model;

namespace Forecast.Data
{
    public class ForecastRepository : IForecastRepository
    {
        public Task<IEnumerable<ServiceForecast>> GetAllForecastByName(string input)
        {
            return Task.FromResult<IEnumerable<ServiceForecast>>(new List<ServiceForecast>());
        }

        public Task<IEnumerable<ServiceForecast>> GetAllForecastByZipCode(string input)
        {
            return Task.FromResult<IEnumerable<ServiceForecast>>(new List<ServiceForecast>());
        }

        public Task<WeatherService.CurrentWeather> GetWeatherByName(string input)
        {
            return Task.FromResult<WeatherService.CurrentWeather>(null);
        }

        public Task<WeatherService.CurrentWeather> GetWeatherByZipCode(string input)
        {
            return Task.FromResult<WeatherService.CurrentWeather>(null);
        }
        
        public Task SaveAllForecast(IEnumerable<ServiceForecast> result)
        {
            return new Task(null);
        }
    }
}