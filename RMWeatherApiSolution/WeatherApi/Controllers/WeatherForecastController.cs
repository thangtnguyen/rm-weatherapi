using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WeatherApi.Models.Entities;
using WeatherApi.Models.Interfaces;

namespace WeatherApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly ICacheService _cacheService;
        private readonly IWeatherApiService _weatherApiService;

        public WeatherForecastController(ICacheService cacheService, IWeatherApiService weatherApiService, ILogger<WeatherForecastController> logger)
        {
            _cacheService = cacheService;
            _weatherApiService = weatherApiService;
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get([Required] string location)
        {
            var result = await _weatherApiService.GetWeatherForecasts(location);

            return result;
        }
    }
}
