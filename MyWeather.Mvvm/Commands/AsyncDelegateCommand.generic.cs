namespace MyWeather.Mvvm.Commands
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class AsyncDelegateCommand<T> : ICommand
    {
        private readonly Func<T, Task> executeAction;
        private readonly Predicate<T> canExecuteAction;

        public event EventHandler CanExecuteChanged;

        public AsyncDelegateCommand(Func<T, Task> execute)
            : this(execute, null)
        {
        }

        public AsyncDelegateCommand(Func<T, Task> executeAction,
                       Predicate<T> canExecuteAction)
        {
            this.executeAction = executeAction;
            this.canExecuteAction = canExecuteAction;
        }

        public bool CanExecute(object parameter)
        {
            if (this.canExecuteAction == null)
            {
                return true;
            }

            return this.canExecuteAction((T)parameter);
        }

        public async void Execute(object parameter)
        {
            await this.ExecuteAsync((T)parameter);
        }

        protected virtual async Task ExecuteAsync(T parameter)
        {
            await this.executeAction(parameter);
        }
    }
}
