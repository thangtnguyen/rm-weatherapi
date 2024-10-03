namespace WeatherApi.Models.Entities
{
    public class WeatherWrapper
    {
        public string Address { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<WeatherForecast> Days { get; set; } = new();
    }
}
