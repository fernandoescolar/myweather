namespace MyWeather.Tests
{
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

    public static class Assume
    {
        public static void IsNotNull(object obj)
        {
            Assert.IsNotNull(obj);
        }
    }
}