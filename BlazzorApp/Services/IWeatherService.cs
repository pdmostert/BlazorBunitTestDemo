using BlazzorApp.Model;

public interface IWeatherService
{
    Task<WeatherForecast[]> GetForecastAsync();
}
