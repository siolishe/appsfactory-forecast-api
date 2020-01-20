using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<ServiceForecast>> GetAllForecastByName(string input)
        {
            var result = await _repository.GetAllForecastByName(input);
            if (!result.Any())
            {
                result = await _client.GetAllForecastByName(input);
                await _repository.SaveAllForecast(result);
            }

            return result;
        }

        public async Task<IEnumerable<ServiceForecast>> GetAllForecastByZipCode(string input)
        {
            var result = await _repository.GetAllForecastByZipCode(input);
            if (!result.Any())
            {
                result = await _client.GetAllForecastByZipCode(input);
                await _repository.SaveAllForecast(result);
            }

            return result;
        }

        public async Task<WeatherService> GetWeatherByName(string input)
        {
            var result = await _repository.GetWeatherByName(input);
            if (result == null)
            {
                result = await _client.GetWeatherByName(input);
                // await _repository.SaveAllForecast(result);
            }

            return result;
        }

        public async Task<WeatherService> GetWeatherByZipCode(string input)
        {
            var result = await _repository.GetWeatherByZipCode(input);
            if (result is null)
            {
                result = await _client.GetWeatherByZipCode(input);
                // await _repository.SaveAllForecast(result);
            }

            return result;
        }
    }
}