namespace MyWeather.Core.Services
{
    using Model;
    using Windows.ApplicationModel.Resources;

    public class Resources : IResources
    {
        private const string ResourcesUrl = "MyWeather.Resources/Strings";
        private const string HumidityFormat = "HumidityFormat";
        private const string TemperatureFormat = "TemperatureFormat";
        private const string WindFormat = "WindFormat";
        private const string ImageResourceFormat = "ImageResourceFormat";
        private const string ImageExtension = ".png";

        private const string ColdPhrase = "ColdPhrase";
        private const string HotPhrase = "HotPhrase";
        private const string GreatDayPhrase = "GreatDayPhrase";
        private const string GreatPhrase = "GreatPhrase";
        private const string TinyRainPhrase = "TinyRainPhrase";
        private const string RainingPhrase = "RainingPhrase";
        private const string GrayPhrase = "GrayPhrase";
        private const string ReallyBadPhrase = "ReallyBadPhrase";
        private const string DefaultPhrase = "DefaultPhrase";

        private readonly ResourceLoader resourceLoader;

        public string LoadingPhrase { get { return this.GetString(nameof(LoadingPhrase)); } }

        public string Today { get { return this.GetString(nameof(Today)); } }

        public string Tomorrow { get { return this.GetString(nameof(Tomorrow)); } }

        public string NextDay { get { return this.GetString(nameof(NextDay)); } }

        public Resources()
        {
            resourceLoader = ResourceLoader.GetForViewIndependentUse(ResourcesUrl);
        }

        public string GetFormattedHumidity(double humidity)
        {
            return string.Format(this.GetString(HumidityFormat), humidity);
        }

        public string GetFormattedTemperature(double temperature)
        {
            return string.Format(this.GetString(TemperatureFormat), temperature);
        }

        public string GetFormattedWind(double speed, double angle)
        {
            return string.Format(this.GetString(WindFormat), speed, angle);
        }

        public string GetImageResource(string icon)
        {
            return string.Format(this.GetString(ImageResourceFormat), icon.Replace(ImageExtension, string.Empty));
        }

        public string GetPhrase(CurrentWeather model)
        {
            return this.GetString(GetPhraseResource(model));
        }

        public string GetString(string key)
        {
            if (resourceLoader != null)
            {
                var result = resourceLoader.GetString(key);
                if (result != null) return result.Replace("\\n", "\n"); //Fix the linebreaks
            }

            return key;
        }

        private static string GetPhraseResource(CurrentWeather model)
        {
            if (model.IsCold()) return ColdPhrase;
            if (model.IsHot()) return HotPhrase;
            if (model.IsAGreatDay()) return GreatDayPhrase;
            if (model.IsGreat()) return GreatPhrase;
            if (model.IsDrizzling()) return TinyRainPhrase;
            if (model.IsRaining()) return RainingPhrase;
            if (model.IsGray()) return GrayPhrase;
            if (model.IsReallyBad()) return ReallyBadPhrase;

            return DefaultPhrase;
        }
    }
}
