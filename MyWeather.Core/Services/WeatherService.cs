namespace MyWeather.Core.Services
{
    using Model;
    using Newtonsoft.Json;
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Windows.Devices.Geolocation;

    public class WeatherService : IWeatherService
    {
        private const string WeatherApiKey = "157e30de6320f9112a8784a791390eea";
        private const string WeatherApi = "http://api.openweathermap.org/data/2.5/forecast/daily?type=accurate&units=metric&cnt=3&appid=" + WeatherApiKey;
        private const string CurrentWeatherApi = "http://api.openweathermap.org/data/2.5/weather?type=accurate&units=metric&cnt=3&appid=" + WeatherApiKey;
        private const string WeatherQueryApi = WeatherApi + "&q={0}";
        private const string WeatherCoordinateApi = WeatherApi + "&lat={0}&lon={1}";
        private const string CurrentWeatherQueryApi = CurrentWeatherApi + "&q={0}";
        private const string CurrentWeatherCoordinateApi = CurrentWeatherApi + "&lat={0}&lon={1}";

        public Task<WeatherInfo> GetCityWeatherAsync(string cityName)
        {
            var url = string.Format(WeatherQueryApi, cityName);
            return GetAsync<WeatherInfo>(url);
        }

        public Task<WeatherInfo> GetCoordinateWeatherAsync(double latitude, double longitude)
        {
            var url = string.Format(WeatherCoordinateApi, latitude, longitude);
            return GetAsync<WeatherInfo>(url);
        }

        public async Task<WeatherInfo> GetWeatherAsync()
        {
            var coordinates = await this.GetCoordinatesAsync();
            return await this.GetCoordinateWeatherAsync(coordinates.Latitude, coordinates.Longitude);
        }

        public Task<CurrentWeather> GetCurrentCityWeatherAsync(string cityName)
        {
            var url = string.Format(CurrentWeatherQueryApi, cityName);
            return GetAsync<CurrentWeather>(url);
        }

        public Task<CurrentWeather> GetCoordinateCurrentWeatherAsync(double latitude, double longitude)
        {
            var url = string.Format(CurrentWeatherCoordinateApi, latitude, longitude);
            return GetAsync<CurrentWeather>(url);
        }

        public async Task<CurrentWeather> GetCurrentWeatherAsync()
        {
            var coordinates = await this.GetCoordinatesAsync();
            return await this.GetCoordinateCurrentWeatherAsync(coordinates.Latitude, coordinates.Longitude);
        }

        private async Task<Coordinates> GetCoordinatesAsync()
        {
            var result = new Coordinates();
            var accessStatus = await Geolocator.RequestAccessAsync();
            if (accessStatus == GeolocationAccessStatus.Allowed)
            {
                var geolocator = new Geolocator { DesiredAccuracyInMeters = 200 };
                var geoposition = await geolocator.GetGeopositionAsync();
                result.Latitude = geoposition.Coordinate.Point.Position.Latitude;
                result.Longitude = geoposition.Coordinate.Point.Position.Longitude;
            }

            return result;
        }

        private static async Task<T> GetAsync<T>(string url)
        {
            using (var client = CreateClient(url))
            {
                var response = await client.GetAsync(new Uri(url));
                response.EnsureSuccessStatusCode();

                return await ProcessResponse<T>(response);
            }
        }

        private static async Task<T> ProcessResponse<T>(HttpResponseMessage message)
        {
            message.EnsureSuccessStatusCode();
            var responseContent = await message.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        private static HttpClient CreateClient(string url)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
