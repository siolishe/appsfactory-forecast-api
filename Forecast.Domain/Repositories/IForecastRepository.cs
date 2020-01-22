using System.Collections.Generic;
using System.Threading.Tasks;
using Forecast.Model;

namespace Forecast.Domain.Repositories
{
    public interface IForecastRepository
    {
        Task<IEnumerable<Model.Forecast>> GetAllForecastByName(string input);
        Task<IEnumerable<Model.Forecast>> GetAllForecastByZipCode(string input);
        Task<WeatherService.CurrentWeather> GetWeatherByName(string input);
        Task<WeatherService.CurrentWeather> GetWeatherByZipCode(string input);
        Task SaveAllForecast(IEnumerable<Model.Forecast> result);
    }
}