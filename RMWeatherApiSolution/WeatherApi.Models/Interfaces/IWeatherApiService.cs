using System.Data;
using WeatherApi.Models.Entities;

namespace WeatherApi.Models.Interfaces
{
    public interface IWeatherApiService
    {
        Task<List<WeatherForecast>> GetWeatherForecasts(string location);
    }
}
