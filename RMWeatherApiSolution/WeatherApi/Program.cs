using WeatherApi.Models;
using WeatherApi.Models.Interfaces;
using WeatherApi.Models.Options;
using WeatherApi.RedisCacheService;
using WeatherApi.VisualCrossingService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient(Constants.HTTP_CLIENT_NAME, httpClient =>
{
    var visualCrossingApiOptions = new VisualCrossingOptions();
    builder.Configuration.GetSection(VisualCrossingOptions.VisualCrossingApi).Bind(visualCrossingApiOptions);
    httpClient.BaseAddress = new Uri(visualCrossingApiOptions.Url);
});
builder.Services.AddSingleton<ICacheService, RedisCacheService>();
builder.Services.AddScoped<IWeatherApiService, VisualCrossingApiService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
