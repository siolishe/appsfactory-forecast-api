using System;
using System.Linq;
using System.Threading.Tasks;
using Forecast.Domain.Repositories;
using Forecast.Domain.Services;
using Forecast.Model;

namespace Forecast.Service
{
    public class ForecastService : IForecastService
    {
        private readonly IForecastClient _client;
        private readonly IForecastRepository _repository;

        public ForecastService(IForecastClient client, IForecastRepository repository)
        {
            _client = client;
            _repository = repository;
        }

        public async Task<Model.Forecast> GetAllForecastByName(string input)
        {
            var result = await _repository.GetAllForecastByName(input);

            if (result is null)
                return null;

            result = await _client.GetAllForecastByName(input);
            var list = result.list.GroupBy(x => x.dt_txt.Split(' ').First()).ToList();
            result.list = list.Select(item => item.First()).ToList();
            foreach (var item in result.list)
            {
                item.main.temp_min = (list.FirstOrDefault(x => x.Key == item.dt_txt.Split(' ').First()) ??
                                      throw new InvalidOperationException())
                    .Min(x => x.main.temp_min);
                item.main.temp_max = (list.FirstOrDefault(x => x.Key == item.dt_txt.Split(' ').First()) ??
                                      throw new InvalidOperationException())
                    .Max(x => x.main.temp_min);
            }
            //await _repository.SaveAllForecast(result);

            return result;
        }

        public async Task<Model.Forecast> GetAllForecastByZipCode(string input)
        {
            var result = await _repository.GetAllForecastByZipCode(input);

            if (result is null)
                return null;

            result = await _client.GetAllForecastByZipCode(input);
            var list = result.list.GroupBy(x => x.dt_txt.Split(' ').First()).ToList();
            result.list = list.Select(item => item.First()).ToList();
            foreach (var item in result.list)
            {
                item.main.temp_min = (list.FirstOrDefault(x => x.Key == item.dt_txt.Split(' ').First()) ??
                                      throw new InvalidOperationException())
                    .Min(x => x.main.temp_min);
                item.main.temp_max = (list.FirstOrDefault(x => x.Key == item.dt_txt.Split(' ').First()) ??
                                      throw new InvalidOperationException())
                    .Max(x => x.main.temp_min);
            }
            //await _repository.SaveAllForecast(result);

            return result;
        }

        public async Task<WeatherService.CurrentWeather> GetWeatherByName(string input)
        {
            var result = await _repository.GetWeatherByName(input)
                         ?? await _client.GetWeatherByName(input);

            return result;
        }

        public async Task<WeatherService.CurrentWeather> GetWeatherByZipCode(string input)
        {
            var result = await _repository.GetWeatherByZipCode(input) ??
                         await _client.GetWeatherByZipCode(input);

            return result;
        }

        public async Task<Model.Forecast> GetChartDataByName(string city)
        {
            var result = await _repository.GetAllForecastByName(city);
            if (!(result is null))
                result = await _client.GetAllForecastByName(city);
            //await _repository.SaveAllForecast(result);

            return result;
        }

        public async Task<Model.Forecast> GetChartDataByZipCode(string zipcode)
        {
            var result = await _repository.GetAllForecastByName(zipcode);
            if (!(result is null))
                result = await _client.GetAllForecastByName(zipcode);
            //await _repository.SaveAllForecast(result);

            return result;
        }
    }
}