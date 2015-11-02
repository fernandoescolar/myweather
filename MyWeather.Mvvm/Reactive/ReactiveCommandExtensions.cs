namespace MyWeather.Mvvm.Reactive
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Reactive.Concurrency;

    public static class ReactiveCommandExtensions
    {
        /// <summary>
        /// CanExecuteChanged is called from canExecute sequence on UIDispatcherScheduler.
        /// </summary>
        public static ReactiveCommand ToReactiveCommand(this IObservable<bool> canExecuteSource, bool initialValue = true)
        {
            Contract.Requires<ArgumentNullException>(canExecuteSource != null);
            Contract.Ensures(Contract.Result<ReactiveCommand>() != null);

            return new ReactiveCommand(canExecuteSource, initialValue);
        }

        /// <summary>
        /// CanExecuteChanged is called from canExecute sequence on scheduler.
        /// </summary>
        public static ReactiveCommand ToReactiveCommand(this IObservable<bool> canExecuteSource, IScheduler scheduler, bool initialValue = true)
        {
            Contract.Requires<ArgumentNullException>(canExecuteSource != null);
            Contract.Requires<ArgumentNullException>(scheduler != null);
            Contract.Ensures(Contract.Result<ReactiveCommand>() != null);

            return new ReactiveCommand(canExecuteSource, scheduler, initialValue);
        }

        /// <summary>
        /// CanExecuteChanged is called from canExecute sequence on UIDispatcherScheduler.
        /// </summary>
        public static ReactiveCommand<T> ToReactiveCommand<T>(this IObservable<bool> canExecuteSource, bool initialValue = true)
        {
            Contract.Requires<ArgumentNullException>(canExecuteSource != null);
            Contract.Ensures(Contract.Result<ReactiveCommand<T>>() != null);

            return new ReactiveCommand<T>(canExecuteSource, initialValue);
        }

        /// <summary>
        /// CanExecuteChanged is called from canExecute sequence on scheduler.
        /// </summary>
        public static ReactiveCommand<T> ToReactiveCommand<T>(this IObservable<bool> canExecuteSource, IScheduler scheduler, bool initialValue = true)
        {
            Contract.Requires<ArgumentNullException>(canExecuteSource != null);
            Contract.Requires<ArgumentNullException>(scheduler != null);
            Contract.Ensures(Contract.Result<ReactiveCommand<T>>() != null);

            return new ReactiveCommand<T>(canExecuteSource, scheduler, initialValue);
        }
    }
}