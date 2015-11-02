namespace MyWeather.Core.Services
{
    using Model;
    using System.Threading.Tasks;

    public interface IWeatherService
    {
        Task<WeatherInfo> GetCityWeatherAsync(string cityName);

        Task<WeatherInfo> GetCoordinateWeatherAsync(double latitude, double longitude);

        Task<WeatherInfo> GetWeatherAsync();

        Task<CurrentWeather> GetCurrentCityWeatherAsync(string cityName);

        Task<CurrentWeather> GetCoordinateCurrentWeatherAsync(double latitude, double longitude);

        Task<CurrentWeather> GetCurrentWeatherAsync();
    }
}
