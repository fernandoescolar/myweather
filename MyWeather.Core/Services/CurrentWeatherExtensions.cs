namespace MyWeather.Core.Services
{
    using Model;

    public static class CurrentWeatherExtensions
    {
        public static bool IsReallyBad(this CurrentWeather model)
        {
            return model.Weather[0].Id >= 900 && model.Weather[0].Id < 1000;
        }

        public static bool IsGray(this CurrentWeather model)
        {
            return model.Weather[0].Id >= 801 && model.Weather[0].Id < 900;
        }

        public static bool IsRaining(this CurrentWeather model)
        {
            return (model.Weather[0].Id >= 200 && model.Weather[0].Id < 300) || (model.Weather[0].Id > 501 && model.Weather[0].Id < 600);
        }

        public static bool IsDrizzling(this CurrentWeather model)
        {
            return (model.Weather[0].Id >= 300 && model.Weather[0].Id < 400) || model.Weather[0].Id == 500 || model.Weather[0].Id == 501;
        }

        public static bool IsGreat(this CurrentWeather model)
        {
            return model.MainInfo.Temperature >= 20 && model.MainInfo.Temperature <= 25;
        }

        public static bool IsAGreatDay(this CurrentWeather model)
        {
            return model.MainInfo.Temperature >= 20 && model.MainInfo.Temperature <= 25 && model.Weather[0].Icon == "01d.png";
        }

        public static bool IsHot(this CurrentWeather model)
        {
            return model.MainInfo.Temperature > 30;
        }

        public static bool IsCold(this CurrentWeather model)
        {
            return model.MainInfo.Temperature < 5
                || (model.Weather[0].Id >= 600 && model.Weather[0].Id < 700);
        }
    }
}
