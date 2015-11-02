namespace MyWeather.Mvvm.Reactive
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reactive.Subjects;

    public class ReactiveProperty<T> : IObservable<T>, INotifyPropertyChanged
    {
        private readonly Subject<T> changes = new Subject<T>();
        private Action<T> setter;
        private Func<T> getter;

        public event PropertyChangedEventHandler PropertyChanged ;

        public ReactiveProperty(T initialValue = default(T))
        {
            var innerValue = initialValue;
            this.Initialize(v => innerValue = v, () => innerValue);
        }

        public ReactiveProperty(Expression<Func<T>> propertyExpression)
        {
            var parameter = Expression.Parameter(typeof (T));
            var mySetter = Expression.Lambda<Action<T>>(Expression.Assign(propertyExpression.Body as MemberExpression, parameter), parameter);
            this.Initialize(mySetter.Compile(), propertyExpression.Compile());
        }

        public ReactiveProperty(Action<T> setter, Func<T> getter)
        {
            this.Initialize(setter, getter);
        }

        public T Value
        {
            get { return this.getter(); }
            set { this.OnNext(value); }
        }

        public IDisposable SubscribeTo(IObservable<T> source)
        {
            return source.Subscribe(this.OnNext, this.changes.OnError);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return this.changes.Subscribe(observer);
        }

        public IDisposable Subscribe(Action<T> action)
        {
            return this.changes.Subscribe(action);
        }

        protected void OnNext(T next)
        {
            if (Equals(this.getter(), next))
                return;

            this.setter(next);

            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs("Value"));
            }

            this.changes.OnNext(next);
        }

        private void Initialize(Action<T> setter, Func<T> getter)
        {
            this.setter = setter;
            this.getter = getter;
        }
    }
}
