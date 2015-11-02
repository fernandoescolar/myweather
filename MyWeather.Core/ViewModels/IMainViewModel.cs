namespace MyWeather.Core.ViewModels
{
    using System.Windows.Input;
    using MyWeather.Mvvm.Reactive;
    using Mvvm.Commands;

    public interface IMainViewModel
    {
        ReactiveProperty<string> City { get; }
        ReactiveProperty<string> CurrentImageResource { get; }
        ReactiveProperty<string> CurrentName { get; }
        ReactiveProperty<string> Humidity { get; }
        ReactiveProperty<string> ImageResource { get; }
        ReactiveProperty<bool> IsLoading { get; }
        ReactiveProperty<string> NextImageResource { get; }
        ReactiveProperty<string> NextName { get; }
        ReactiveProperty<string> Phrase { get; }
        ReactiveProperty<string> PreviousImageResource { get; }
        ReactiveProperty<string> PreviousName { get; }
        AsyncDelegateCommand RefreshCommand { get; }
        ReactiveProperty<string> Temperature { get; }
        ReactiveProperty<string> Wind { get; }
    }
}