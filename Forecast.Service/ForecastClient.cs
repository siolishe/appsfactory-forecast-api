using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Forecast.Domain.Services;
using Forecast.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Forecast.Service
{
    public class ForecastClient : IForecastClient
    {
        public async Task<Model.Forecast> GetAllForecastByName(string input)
        {
            var url = $"https://api.openweathermap.org/data/2.5/forecast?q={input}";
            var result = await MakeApiCall<Model.Forecast>(new Uri(url),
                string.Empty);
            return result;
        }

        public async Task<Model.Forecast> GetAllForecastByZipCode(string input)
        {
            var url = $"https://api.openweathermap.org/data/2.5/forecast?q={input}";
            var result = await MakeApiCall<Model.Forecast>(new Uri(url),
                string.Empty);
            return result;
        }

        public async Task<WeatherService.CurrentWeather> GetWeatherByName(string input)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={input}";
            var result = await MakeApiCall<WeatherService.CurrentWeather>(new Uri(url),
                string.Empty);
            return result;
        }

        public async Task<WeatherService.CurrentWeather> GetWeatherByZipCode(string input)
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?zip={input}";
            var result = await MakeApiCall<WeatherService.CurrentWeather>(new Uri(url),
                string.Empty);
            return result;
        }

        private async Task<T> MakeApiCall<T>(Uri endpoint, string data, string method = "GET",
            int requestTimeOut = 30000) where T : class
        {
            try
            {
                var request = (HttpWebRequest) WebRequest.Create(string.Concat(endpoint.OriginalString,
                    "&units=metric&apikey=fcadd28326c90c3262054e0e6ca599cd"));
                request.ContentType = "application/json";
                request.Method = method;
                request.Timeout = requestTimeOut;
                request.KeepAlive = false;

                if (!string.IsNullOrEmpty(data))
                {
                    await using var sw = new StreamWriter(request.GetRequestStream());
                    sw.Write(data);
                    sw.Close();
                }

                string responseString;
                using (var webResponse = await request.GetResponseAsync())
                {
                    await using (var response = webResponse.GetResponseStream())
                    {
                        if (response is null || response.Equals(Stream.Null)) throw new Exception("Response is null");

                        using (var stream = new StreamReader(response, Encoding.UTF8))
                        {
                            responseString = await stream.ReadToEndAsync();
                            stream.Close();
                        }

                        response.Close();
                    }

                    webResponse.Close();
                }

                if (string.IsNullOrEmpty(responseString))
                    throw new Exception("Response is empty");

                var responseType = typeof(T);

                var result = responseType.IsGenericType
                    ? JsonConvert.DeserializeObject<T>(JObject.Parse(responseString).ToString())
                    : JsonConvert.DeserializeObject<T>(responseString);

                return result;
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(WebException)) ((WebException) ex).Response.Close();

                throw;
            }
        }
    }
}