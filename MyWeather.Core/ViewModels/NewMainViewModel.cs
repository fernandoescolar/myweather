namespace MyWeather.Core.ViewModels
{
    using Model;
    using Mvvm.Base;
    using Mvvm.Commands;
    using Mvvm.Reactive;
    using Services;
    using System;
    using System.Threading.Tasks;
    using Windows.UI.Popups;

    public class NewMainViewModel : ViewModel, IMainViewModel
    {
        private readonly IWeatherService service;
        private readonly IResources resources;

        [RestorableState]
        public ReactiveProperty<string> City { get; private set; }

        [RestorableState]
        public ReactiveProperty<string> Temperature { get; private set; }

        [RestorableState]
        public ReactiveProperty<string> Wind { get; private set; }

        [RestorableState]
        public ReactiveProperty<string> Humidity { get; private set; }

        [RestorableState]
        public ReactiveProperty<string> ImageResource { get; private set; }

        [RestorableState]
        public ReactiveProperty<string> PreviousImageResource { get; private set; }

        [RestorableState]
        public ReactiveProperty<string> CurrentImageResource { get; private set; }

        [RestorableState]
        public ReactiveProperty<string> NextImageResource { get; private set; }

        [RestorableState]
        public ReactiveProperty<string> PreviousName { get; private set; }

        [RestorableState]
        public ReactiveProperty<string> CurrentName { get; private set; }

        [RestorableState]
        public ReactiveProperty<string> NextName { get; private set; }

        [RestorableState]
        public ReactiveProperty<string> Phrase { get; private set; }

        public ReactiveProperty<bool> IsLoading { get; private set; }

        public AsyncDelegateCommand RefreshCommand { get; private set; }

        public NewMainViewModel(IWeatherService service, IResources resources)
        {
            this.service = service;
            this.resources = resources;
            this.City = new ReactiveProperty<string>();
            this.Temperature = new ReactiveProperty<string>();
            this.Wind = new ReactiveProperty<string>();
            this.Humidity = new ReactiveProperty<string>();
            this.ImageResource = new ReactiveProperty<string>();
            this.PreviousImageResource = new ReactiveProperty<string>();
            this.CurrentImageResource = new ReactiveProperty<string>();
            this.NextImageResource = new ReactiveProperty<string>();
            this.PreviousName = new ReactiveProperty<string>();
            this.CurrentName = new ReactiveProperty<string>();
            this.NextName = new ReactiveProperty<string>();
            this.Phrase = new ReactiveProperty<string>(this.resources.LoadingPhrase);
            this.IsLoading = new ReactiveProperty<bool>();
            this.RefreshCommand = new AsyncDelegateCommand(_ => this.Reload());
        }

        protected override async void OnLoad(object parameter)
        {
            base.OnLoad(parameter);
            await this.Reload();

        }

        private async Task Reload()
        {
            this.IsLoading.Value = true;
            try
            {
                var current = await this.service.GetCurrentWeatherAsync();
                this.Parse(current);

                var prediction = await this.service.GetWeatherAsync();
                this.Parse(prediction);
            }
            catch (Exception ex)
            {
                var messageDialog = new MessageDialog(ex.Message);
                await messageDialog.ShowAsync();
            }
            finally
            {
                this.IsLoading.Value = false;
            }
        }

        private void Parse(CurrentWeather model)
        {
            this.City.Value = model.Name;
            this.Temperature.Value = this.resources.GetFormattedTemperature(model.MainInfo.Temperature);
            this.Wind.Value = this.resources.GetFormattedWind(model.Wind.Speed, model.Wind.Deg);
            this.Humidity.Value = this.resources.GetFormattedHumidity(model.MainInfo.Humidity);
            this.ImageResource.Value = this.resources.GetImageResource(model.Weather[0].Icon);
            this.Phrase.Value = this.resources.GetPhrase(model);
        }

        private void Parse(WeatherInfo model)
        {
            this.PreviousImageResource.Value = this.resources.GetImageResource(model.Results[0].Weather[0].Icon);
            this.CurrentImageResource.Value = this.resources.GetImageResource(model.Results[1].Weather[0].Icon);
            this.NextImageResource.Value = this.resources.GetImageResource(model.Results[2].Weather[0].Icon);
            this.PreviousName.Value = this.resources.Today;
            this.CurrentName.Value = this.resources.Tomorrow;
            this.NextName.Value = this.resources.NextDay;
        }
    }
}
