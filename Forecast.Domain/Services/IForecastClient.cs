using System.Threading.Tasks;
using Forecast.Model;

namespace Forecast.Domain.Services
{
    public interface IForecastClient
    {
        Task<Model.Forecast> GetAllForecastByName(string input);
        Task<Model.Forecast> GetAllForecastByZipCode(string input);
        Task<WeatherService.CurrentWeather> GetWeatherByName(string input);
        Task<WeatherService.CurrentWeather> GetWeatherByZipCode(string input);
    }
}