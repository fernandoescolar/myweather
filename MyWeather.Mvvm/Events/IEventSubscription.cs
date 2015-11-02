namespace MyWeather.Mvvm.Events
{
    using System;

    public interface IEventSubscription
    {
        Type TypeOfEvent { get; }
    }
}