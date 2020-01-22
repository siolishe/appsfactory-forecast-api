using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Forecast.Domain.Repositories;
using Forecast.Model;

namespace Forecast.Data
{
    public class ForecastRepository : IForecastRepository
    {
        public Task<IEnumerable<Model.Forecast>> GetAllForecastByName(string input)
        {
            return Task.FromResult<IEnumerable<Model.Forecast>>(new List<Model.Forecast>());
        }

        public Task<IEnumerable<Model.Forecast>> GetAllForecastByZipCode(string input)
        {
            return Task.FromResult<IEnumerable<Model.Forecast>>(new List<Model.Forecast>());
        }

        public Task<WeatherService.CurrentWeather> GetWeatherByName(string input)
        {
            return Task.FromResult<WeatherService.CurrentWeather>(null);
        }

        public Task<WeatherService.CurrentWeather> GetWeatherByZipCode(string input)
        {
            return Task.FromResult<WeatherService.CurrentWeather>(null);
        }
        
        public Task SaveAllForecast(IEnumerable<Model.Forecast> result)
        {
            return new Task(null);
        }
    }
}