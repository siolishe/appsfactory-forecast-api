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

        public Task<WeatherService> GetWeatherByName(string input)
        {
            return Task.FromResult(new WeatherService());
        }

        public Task<WeatherService> GetWeatherByZipCode(string input)
        {
            return Task.FromResult(new WeatherService());
        }
        
        public Task SaveAllForecast(IEnumerable<ServiceForecast> result)
        {
            return new Task(null);
        }
    }
}