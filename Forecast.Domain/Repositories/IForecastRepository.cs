using System.Collections.Generic;
using System.Threading.Tasks;
using Forecast.Model;

namespace Forecast.Domain.Repositories
{
    public interface IForecastRepository
    {
        Task<IEnumerable<ServiceForecast>> GetAllForecastByName(string input);
        Task<IEnumerable<ServiceForecast>> GetAllForecastByZipCode(string input);
        Task<WeatherService> GetWeatherByName(string input);
        Task<WeatherService> GetWeatherByZipCode(string input);
        Task SaveAllForecast(IEnumerable<ServiceForecast> result);
    }
}