namespace MyWeather.Mvvm.Events
{
    using System;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Threading.Tasks;

    internal sealed class ReactiveEventAggregator : IEventAggregator
    {
        private readonly ConcurrentDictionary<Type, object> subjects;

        public ReactiveEventAggregator()
        {
            this.subjects = new ConcurrentDictionary<Type, object>();
        }

        public IEventSubscription Subscribe<TEvent>(Action<TEvent> handler)
        {
            var disposable = this.GetEvent<TEvent>().Subscribe(handler);
            return new ReactiveEventSubscription(typeof(TEvent), disposable);
        }

        public IEventSubscription Subscribe(Type type, Action<object> handler)
        {
            throw new NotImplementedException();
        }

        public IEventSubscription SubscribeAsync<TEvent>(Func<TEvent, Task> handler)
        {
            var disposable = this.GetEvent<TEvent>().Subscribe(async o => await handler(o));
            return new ReactiveEventSubscription(typeof(TEvent), disposable);
        }

        public IEventSubscription SubscribeAsync(Type type, Func<object, Task> handler)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(IEventSubscription eventSubscription)
        {
            if (eventSubscription is ReactiveEventSubscription)
            {
                var subscription = (ReactiveEventSubscription) eventSubscription;
                if (subscription.DisposableObject != null)
                {
                    subscription.DisposableObject.Dispose();
                }
            }
        }

        public void Notify<TEvent>(TEvent data)
        {
            object handler;
            if (this.subjects.TryGetValue(typeof(TEvent), out handler))
            {
                if (handler is ISubject<TEvent>)
                {
                    ((ISubject<TEvent>) handler).OnNext(data);
                }
            }
        }

        private IObservable<T> GetEvent<T>()
        {
            return this.GetOrAdd<T>().AsObservable();
        }

        private ISubject<T> GetOrAdd<T>()
        {
            return (ISubject<T>)this.subjects.GetOrAdd(typeof(T), _ => new Subject<T>());
        }
    } 
}