namespace MyWeather.Mvvm.Events
{
    using System;

    internal sealed class ReactiveEventSubscription : IEventSubscription
    {
        public Type TypeOfEvent { get; private set; }

        public IDisposable DisposableObject { get; private set; }

        public ReactiveEventSubscription(Type type, IDisposable disposable)
        {
            this.TypeOfEvent = type;
            this.DisposableObject = disposable;
        }
    }
}
