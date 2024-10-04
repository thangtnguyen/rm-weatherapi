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
            var upperLocation = location.ToUpper();

            var cachedData = _cacheService.GetData<List<WeatherForecast>>(upperLocation);
            if (cachedData != null)
            {
                return cachedData;
            }

            var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
            var result = await _weatherApiService.GetWeatherForecasts(upperLocation);
            _cacheService.SetData<List<WeatherForecast>>(upperLocation, result, expirationTime);

            return result;
        }
    }
}
