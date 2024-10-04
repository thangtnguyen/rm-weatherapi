using Microsoft.Extensions.Configuration;
using System.Text.Json;
using WeatherApi.Models;
using WeatherApi.Models.Entities;
using WeatherApi.Models.Interfaces;
using WeatherApi.Models.Options;

namespace WeatherApi.VisualCrossingService
{
    public class VisualCrossingApiService : IWeatherApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public VisualCrossingApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<List<WeatherForecast>> GetWeatherForecasts(string location)
        {
            var visualCrossingApiOptions = new VisualCrossingOptions();
            _configuration.GetSection(VisualCrossingOptions.VisualCrossingApi).Bind(visualCrossingApiOptions);

            var httpClient = _httpClientFactory.CreateClient(Constants.HTTP_CLIENT_NAME);

            var requestUri = string.Format("{0}?key={1}&unitGroup=metric", Uri.EscapeDataString(location.ToUpper()), visualCrossingApiOptions.ApiKey);

            var httpResponseMessage = await httpClient.GetAsync(requestUri);
            httpResponseMessage.EnsureSuccessStatusCode();
            
            var contentString = await httpResponseMessage.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var wrapper = JsonSerializer.Deserialize<WeatherWrapper>(contentString, options);
            return wrapper!.Days;
        }
    }
}
