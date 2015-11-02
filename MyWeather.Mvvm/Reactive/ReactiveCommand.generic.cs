namespace MyWeather.Mvvm.Reactive
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Reactive.Concurrency;
    using System.Reactive.Linq;
    using System.Reactive.Subjects;
    using System.Windows.Input;

    public class ReactiveCommand<T> : IObservable<T>, ICommand, IDisposable
    {
        public event EventHandler CanExecuteChanged;

        readonly Subject<T> trigger = new Subject<T>();
        readonly IDisposable canExecuteSubscription;
        readonly IScheduler scheduler;
        bool isCanExecute;
        bool isDisposed;

        public ReactiveCommand()
            : this(Observable.Never<bool>())
        {
            Contract.Assume(Observable.Never<bool>() != null);
        }

        public ReactiveCommand(IScheduler scheduler)
            : this(Observable.Never<bool>(), scheduler)
        {
            Contract.Requires<ArgumentNullException>(scheduler != null);
        }

        public ReactiveCommand(IObservable<bool> canExecuteSource, bool initialValue = true)
            : this(canExecuteSource, Scheduler.CurrentThread, initialValue)
        {
            Contract.Requires<ArgumentNullException>(canExecuteSource != null);
        }

        public ReactiveCommand(IObservable<bool> canExecuteSource, IScheduler scheduler, bool initialValue = true)
        {
            Contract.Requires<ArgumentNullException>(canExecuteSource != null);
            Contract.Requires<ArgumentNullException>(scheduler != null);

            this.isCanExecute = initialValue;
            this.scheduler = scheduler;
            this.canExecuteSubscription = canExecuteSource
                .DistinctUntilChanged()
                .ObserveOn(scheduler)
                .Subscribe(b =>
                {
                    this.isCanExecute = b;
                    var handler = this.CanExecuteChanged;
                    if (handler != null) handler(this, EventArgs.Empty);
                });
        }

        public bool CanExecute()
        {
            return this.isCanExecute;
        }

        bool ICommand.CanExecute(object parameter)
        {
            return this.isCanExecute;
        }

        public void Execute(T parameter)
        {
            this.trigger.OnNext(parameter);
        }

        void ICommand.Execute(object parameter)
        {
            this.trigger.OnNext((T)parameter);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return this.trigger.Subscribe(observer);
        }

        public void Dispose()
        {
            if (this.isDisposed) return;

            this.isDisposed = true;
            this.trigger.OnCompleted();
            this.trigger.Dispose();
            this.canExecuteSubscription.Dispose();

            if (this.isCanExecute)
            {
                this.isCanExecute = false;
                this.scheduler.Schedule(() =>
                {
                    this.isCanExecute = false;
                    var handler = this.CanExecuteChanged;
                    if (handler != null) handler(this, EventArgs.Empty);
                });
            }
        }
    }
}
