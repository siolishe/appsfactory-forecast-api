using AppsFactory_WeatherForecast.Helper;
using AutoMapper;
using Forecast.Data;
using Forecast.Domain.Repositories;
using Forecast.Domain.Services;
using Forecast.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace AppsFactory_WeatherForecast
{
    public class Startup
    {
        private const string SpecificOrigins = "_SpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IForecastRepository, ForecastRepository>();
            services.AddScoped<IForecastService, ForecastService>();
            services.AddScoped<IForecastClient, ForecastClient>();
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo {Title = "Forecast API", Version = "v1"});
            });
            services.AddControllers();
            services.AddAutoMapper();
            services.AddCors(options =>
            {
                options.AddPolicy(SpecificOrigins,
                    builder => { builder.WithOrigins("*"); });
            });
            Mapper.Initialize(x => x.AddProfile(typeof(DtoMappingProfile)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();
            app.UseCors(SpecificOrigins);
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppsFactory_WeatherForecast.Forecast.Api");
                c.RoutePrefix = string.Empty;
            });
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}