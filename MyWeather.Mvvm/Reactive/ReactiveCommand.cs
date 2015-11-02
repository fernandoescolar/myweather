namespace MyWeather.Mvvm.Reactive
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Reactive.Concurrency;

    public class ReactiveCommand : ReactiveCommand<object>
    {
        public ReactiveCommand()
        { }

        public ReactiveCommand(IScheduler scheduler)
            : base(scheduler)
        {
            Contract.Requires<ArgumentNullException>(scheduler != null);
        }

        public ReactiveCommand(IObservable<bool> canExecuteSource, bool initialValue = true)
            : base(canExecuteSource, initialValue)
        {
            Contract.Requires<ArgumentNullException>(canExecuteSource != null);
        }

        public ReactiveCommand(IObservable<bool> canExecuteSource, IScheduler scheduler, bool initialValue = true)
            : base(canExecuteSource, scheduler, initialValue)
        {
            Contract.Requires<ArgumentNullException>(canExecuteSource != null);
            Contract.Requires<ArgumentNullException>(scheduler != null);
        }

        public void Execute()
        {
            this.Execute(null);
        }
    }
}
