namespace MyWeather.Tests
{
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
    using Core.ViewModels;
    using Core.Services;
    using System.Threading.Tasks;
    using Core.Model;
    using System.Collections.Generic;

    [TestClass]
    public class OnRefreshMainViewModelTest
    {
        private WeatherServiceMock serviceMock;
        private IMainViewModel target;
        private CurrentWeather dummyCurrentWeather;
        private WeatherInfo dummyWeatherInfo;

        [TestInitialize]
        public void Initialize()
        {
            // arrange
            this.serviceMock = new WeatherServiceMock();
            this.target = new OldMainViewModel(serviceMock);

            this.dummyCurrentWeather = CreateDummyCurrentWeather();
            this.dummyWeatherInfo = CreateWeatherInfo();

            this.serviceMock
                .AddHandlers()
                    .GetCurrentWeatherAsync(() => Task.FromResult(dummyCurrentWeather))
                    .GetWeatherAsync(() => Task.FromResult(dummyWeatherInfo));
        }

        [TestMethod]
        public void CallsWeatherServiceGetCurrentCityWeatherAsync()
        {
            // act
            this.target.RefreshCommand.Execute(this.target);

            // assert
            this.serviceMock.HasBeenCalled().Once().GetCurrentWeatherAsync();
        }

        [TestMethod]
        public void CallsWeatherServiceGetCurrentWeatherAsync()
        {
            // act
            this.target.RefreshCommand.Execute(this.target);

            // assert
            this.serviceMock.HasBeenCalled().Once().GetWeatherAsync();
        }

        [TestMethod]
        public void SetsTheCityName()
        {
            // arrange
            const string expected = "any name";
            this.dummyCurrentWeather.Name = expected;

            // act
            this.target.RefreshCommand.Execute(this.target);

            // assert
            Assert.AreEqual(expected, this.target.City.Value);
        }

        [TestMethod]
        public void SetsTheTemperature()
        {
            // arrange
            const string expected = "12.2 ºC";
            const double temperature = 12.2;
            this.dummyCurrentWeather.MainInfo.Temperature = temperature;

            // act
            this.target.RefreshCommand.Execute(this.target);

            // assert
            Assert.AreEqual(expected, this.target.Temperature.Value);
        }

        [TestMethod]
        public void WhenTheTemperatureIsLessThan5ItIsSoCold()
        {

            // arrange
            const string expected = "freezing\ncold\nlike a\n<red>fucking</red>\nfridge.";
            const double temperature = 4.2;
            this.dummyCurrentWeather.MainInfo.Temperature = temperature;

            // act
            this.target.RefreshCommand.Execute(this.target);

            // assert
            Assert.AreEqual(expected, this.target.Phrase.Value);
        }

        private static CurrentWeather CreateDummyCurrentWeather()
        {
            return new CurrentWeather
            {
                MainInfo = new MainInfo(),
                Weather = new List<Weather> { new Weather { Icon = string.Empty } },
                Wind = new Wind()
            };
        }

        private static WeatherInfo CreateWeatherInfo()
        {
            return new WeatherInfo
            {
              Results = new List<WeatherResult>
              {
                  new WeatherResult { Weather =new List<Weather> { new Weather { Icon = string.Empty } } },
                  new WeatherResult { Weather =new List<Weather> { new Weather { Icon = string.Empty } } },
                  new WeatherResult { Weather =new List<Weather> { new Weather { Icon = string.Empty } } }
              }
            };
        }
    }
}
