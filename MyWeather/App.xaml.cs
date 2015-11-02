namespace MyWeather
{
    using Mvvm;
    using Core.ViewModels;
    using Views;
    using Windows.UI.Xaml.Controls;
    using Core.Services;

    sealed partial class App : MvvmApplication
    {
        public App() : base()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);

            this.InitializeComponent();
        }

        protected override void SetUp(Frame rootFrame)
        {
            this.Register<IResources, Resources>(true);
            this.Register<IWeatherService, WeatherService>(true);
            //this.Register<IMainViewModel, OldMainViewModel>(true);
            this.Register<IMainViewModel, NewMainViewModel>(true);
        }

        protected override void OnLoaded()
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            this.NavigationService.Navigate(typeof(MainView));
        }
    }
}
