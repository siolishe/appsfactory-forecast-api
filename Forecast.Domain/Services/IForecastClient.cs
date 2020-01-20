using System.Collections.Generic;
using System.Threading.Tasks;
using Forecast.Model;

namespace Forecast.Domain.Services
{
    public interface IForecastClient
    {
        Task<IEnumerable<ServiceForecast>> GetAllForecastByName(string input);
        Task<IEnumerable<ServiceForecast>> GetAllForecastByZipCode(string input);
        Task<WeatherService> GetWeatherByName(string input);
        Task<WeatherService> GetWeatherByZipCode(string input);
    }
}