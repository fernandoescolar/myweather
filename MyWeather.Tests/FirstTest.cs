namespace MyWeather.Tests
{
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

    [TestClass]
    public class FirstTest
    {
        public int Sum(int a, int b)
        {
            return a + b;
        }

        [TestInitialize]
        public void OnInitialize()
        {

        }

        [TestCleanup]
        public void OnCleanUp()
        {

        }

        [TestMethod]
        public void TestSum()
        {
            // arange
            const int a = 5;
            const int b = 9;
            const int expected = 14;

            // assume
            Assume.IsNotNull(a);
            Assume.IsNotNull(b);

            // act
            var actual = Sum(a, b);

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}
