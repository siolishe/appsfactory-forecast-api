using System.Threading.Tasks;
using Forecast.Model;

namespace Forecast.Domain.Repositories
{
    public interface IForecastRepository
    {
        Task<Model.Forecast> GetAllForecastByName(string input);
        Task<Model.Forecast> GetAllForecastByZipCode(string input);
        Task<WeatherService.CurrentWeather> GetWeatherByName(string input);
        Task<WeatherService.CurrentWeather> GetWeatherByZipCode(string input);
        Task SaveAllForecast(Model.Forecast result);
    }
}