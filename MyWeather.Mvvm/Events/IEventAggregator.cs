namespace MyWeather.Mvvm.Events
{
    using System;
    using System.Threading.Tasks;

    public interface IEventAggregator
    {
        IEventSubscription Subscribe<TEvent>(Action<TEvent> handler);

        IEventSubscription Subscribe(Type type, Action<object> handler);

        IEventSubscription SubscribeAsync<TEvent>(Func<TEvent, Task> handler);

        IEventSubscription SubscribeAsync(Type type, Func<object, Task> handler);
        
        void Unsubscribe(IEventSubscription eventSubscription);

        void Notify<TEvent>(TEvent data);
    }
}