using System.Collections.Generic;
using System.Threading.Tasks;
using Forecast.Model;

namespace Forecast.Domain.Services
{
    public interface IForecastService
    {
        Task<IEnumerable<ServiceForecast>> GetAllForecastByName(string input);
        Task<IEnumerable<ServiceForecast>> GetAllForecastByZipCode(string input);
        Task<WeatherService.CurrentWeather> GetWeatherByName(string input);
        Task<WeatherService.CurrentWeather> GetWeatherByZipCode(string input);
    }
}