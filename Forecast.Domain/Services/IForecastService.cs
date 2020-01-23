using System.Collections.Generic;
using System.Threading.Tasks;
using Forecast.Model;

namespace Forecast.Domain.Services
{
    public interface IForecastService
    {
        Task<Model.Forecast> GetAllForecastByName(string city);
        Task<Model.Forecast> GetAllForecastByZipCode(string zipcode);
        Task<WeatherService.CurrentWeather> GetWeatherByName(string city);
        Task<WeatherService.CurrentWeather> GetWeatherByZipCode(string zipcode);
        Task<Model.Forecast> GetChartDataByName(string city);
        Task<Model.Forecast> GetChartDataByZipCode(string zipcode);
    }
}