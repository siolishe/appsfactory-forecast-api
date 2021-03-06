using System;
using System.Globalization;
using System.Linq;
using AppsFactory_WeatherForecast.Dto;
using AutoMapper;
using Forecast.Model;
using Weather = AppsFactory_WeatherForecast.Dto.Weather;

namespace AppsFactory_WeatherForecast.Helper
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<WeatherService.CurrentWeather, Weather>()
                .ForMember(d => d.Humidity, opt => opt.MapFrom(s => s.main.humidity))
                .ForMember(d => d.Icon, opt => opt.MapFrom(s => s.weather.FirstOrDefault().icon))
                .ForMember(d => d.Temperature, opt => opt.MapFrom(s => s.main.temp))
                .ForMember(d => d.CityName, opt => opt.MapFrom(s => s.name))
                .ForMember(d => d.MaxTemp, opt => opt.MapFrom(s => s.main.temp_max))
                .ForMember(d => d.MinTemp, opt => opt.MapFrom(s => s.main.temp_min))
                .ForMember(d => d.WindSpeed,
                    opt => opt.MapFrom(s => s.wind.speed.ToString(CultureInfo.InvariantCulture)))
                .ForMember(d => d.Status, opt => opt.MapFrom(s => s.weather.FirstOrDefault().description))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<List, Dto.Forecast>()
                .ForMember(d => d.MaxTemp, opt => opt.MapFrom(s => s.main.temp_max))
                .ForMember(d => d.MinTemp, opt => opt.MapFrom(s => s.main.temp_min))
                .ForMember(d => d.Icon, opt => opt.MapFrom(s => s.weather.FirstOrDefault().icon))
                .ForMember(d => d.DayOfWeek,
                    opt => opt.MapFrom(s => DateTimeOffset.FromUnixTimeSeconds(s.dt).DayOfWeek));

            CreateMap<List, ForecastChart>()
                .ForMember(d => d.MinTemp, opt => opt.MapFrom(s => s.main.temp_min))
                .ForMember(d => d.MaxTemp, opt => opt.MapFrom(s => s.main.temp_max))
                .ForMember(d => d.Time,
                    opt => opt.MapFrom(s => DateTimeOffset.FromUnixTimeSeconds(s.dt).ToString("g")));
        }
    }
}