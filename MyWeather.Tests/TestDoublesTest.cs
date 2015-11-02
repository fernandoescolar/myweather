namespace MyWeather.Tests
{
    using Core.Model;
    using Core.Services;
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
    using System.Threading.Tasks;

    [TestClass]
    public class TestDoublesTest
    {
        private const string CityName = "Barcelona,ES";

        [TestMethod]
        public async Task SpyTest()
        {
            // arange
            var service = new WeatherServiceSpy();

            // act
            await service.GetCityWeatherAsync(CityName);

            // assert
            service.HasBeenCalled().GetCityWeatherAsync();
            service.HasBeenCalled().GetCityWeatherAsync(CityName);
            service.HasBeenCalled().Once().GetCityWeatherAsync();
            service.HasBeenCalled().Once().GetCityWeatherAsync(CityName);

            var invokation = service.GetCalls().First().GetCityWeatherAsync();
            Assert.AreEqual("GetCityWeatherAsync", invokation.Name);
        }

        [TestMethod]
        public async Task StubTest()
        {
            // arange
            var service = new WeatherServiceStub();
            var dummy = new WeatherInfo();
            service
                .AddHandlers()
                    .GetCityWeatherAsync(cityName => Task.FromResult(dummy));

            // act
            var actual = await service.GetCityWeatherAsync(CityName);

            // assert
            Assert.AreEqual(dummy, actual);
        }

        [TestMethod]
        public async Task MockTest()
        {
            // arange
            var service = new WeatherServiceMock();
            service
                .AddVerifications()
                    .GetCityWeatherAsync(CityName)
                    .GetCurrentCityWeatherAsync(CityName);


            // act
            await service.GetCityWeatherAsync(CityName);
            await service.GetCurrentCityWeatherAsync(CityName);

            // asserts
            service.VerifyAll();
        }
    }
}
