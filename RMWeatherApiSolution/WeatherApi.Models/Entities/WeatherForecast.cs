namespace WeatherApi.Models.Entities
{
    public class WeatherForecast
    {
        public DateTime DateTime { get; set; }
        public float Temp { get; set; }
        public string? Description { get; set; }
    }
}
