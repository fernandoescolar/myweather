namespace MyWeather.Core.ViewModels
{
    using Model;
    using Mvvm.Base;
    using Mvvm.Commands;
    using Mvvm.Reactive;
    using Services;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class OldMainViewModel : ViewModel, IMainViewModel
    {
        private readonly IWeatherService service;

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

        public OldMainViewModel(IWeatherService service)
        {
            this.service = service;
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
            this.Phrase = new ReactiveProperty<string>("loading\n<red>fucking</red>\nweather...");
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

            var current = await this.service.GetCurrentWeatherAsync();
            this.Parse(current);

            var prediction = await this.service.GetWeatherAsync();
            this.Parse(prediction);

            this.IsLoading.Value = false;
        }

        private void Parse(CurrentWeather model)
        {
            this.City.Value = model.Name;
            this.Temperature.Value = model.MainInfo.Temperature + " ºC";
            this.Wind.Value = model.Wind.Speed + "km/h " + model.Wind.Deg + "º";
            this.Humidity.Value = model.MainInfo.Humidity + "%";
            this.ImageResource.Value = "Img" + model.Weather[0].Icon.Replace(".png", "");
        
            if (model.MainInfo.Temperature < 5)
                this.Phrase.Value = "freezing\ncold\nlike a\n<red>fucking</red>\nfridge.";
            else if (model.MainInfo.Temperature > 30)
                this.Phrase.Value = "it's too\n<red>damn</red> hot.";
            else if (model.MainInfo.Temperature >= 20 && model.MainInfo.Temperature <= 25 && model.Weather[0].Icon == "01d.png")
                this.Phrase.Value = "<red>fucking</red>\nlove is\nin the air.";
            else if (model.MainInfo.Temperature >= 20 && model.MainInfo.Temperature <= 25)
                this.Phrase.Value = "it's\n<green>fucking</green> whiskey\ntime.";
            else if ((model.Weather[0].Id >= 300 && model.Weather[0].Id < 400) || model.Weather[0].Id == 500 || model.Weather[0].Id == 501)
                this.Phrase.Value = "get your\n<blue>fucking</blue> umbrella.";
            else if ((model.Weather[0].Id >= 200 && model.Weather[0].Id < 300) || (model.Weather[0].Id > 501 && model.Weather[0].Id < 600))
                this.Phrase.Value = "it's\n<blue>fucking</blue> raining\nnow.";
            else if (model.Weather[0].Id >= 600 && model.Weather[0].Id < 700)
                this.Phrase.Value = "freezing\ncold\nlike a\n<red>fucking</red>\nfridge.";
            else if (model.Weather[0].Id >= 801 && model.Weather[0].Id < 900)
                this.Phrase.Value = "just\n<gray>fucking</gray> gray.";
            else if (model.Weather[0].Id >= 900 && model.Weather[0].Id < 1000)
                this.Phrase.Value = "better stay\nat your\n<red>fucking</red> home.";
            else
                this.Phrase.Value = "meh...\njust stay\n<blue>in bed</red>.";
        }

        private void Parse(WeatherInfo model)
        {
            this.PreviousImageResource.Value = "Img" + model.Results[0].Weather[0].Icon.Replace(".png", "");
            this.CurrentImageResource.Value = "Img" + model.Results[1].Weather[0].Icon.Replace(".png", "");
            this.NextImageResource.Value = "Img" + model.Results[2].Weather[0].Icon.Replace(".png", "");
            this.PreviousName.Value = "today";
            this.CurrentName.Value = "tomorrow";
            this.NextName.Value = "next day";
        }
    }
}
