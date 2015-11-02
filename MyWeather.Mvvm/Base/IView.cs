namespace MyWeather.Mvvm.Base
{
    using MyWeather.Mvvm.Events;

    public interface IView
    {
        IEventAggregator EventAggregator { get; set; }
    }
}