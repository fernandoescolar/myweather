namespace MyWeather.Core.Services
{
    using Model;

    public interface IResources
    {
        string LoadingPhrase { get; }

        string Today { get; }

        string Tomorrow { get; }

        string NextDay { get; }

        string GetString(string key);

        string GetFormattedTemperature(double temperature);

        string GetFormattedWind(double speed, double angle);

        string GetImageResource(string icon);

        string GetFormattedHumidity(double humidity);

        string GetPhrase(CurrentWeather model);
    }
}
