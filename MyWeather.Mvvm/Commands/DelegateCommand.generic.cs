namespace MyWeather.Mvvm.Commands
{
    using System;
    using System.Windows.Input;

    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> executeAction;
        private readonly Func<T, bool> canExecuteAction;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<T> action, Func<T, bool> canExecute)
        {
            this.executeAction = action;
            this.canExecuteAction = canExecute;
        }

        public DelegateCommand(Action<T> action)
            : this(action, _ => true)
        {
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecuteAction((T)parameter);
        }

        public void Execute(object parameter)
        {
            this.executeAction((T)parameter);
        }

        public void OnCanExecuteChanged()
        {
            var handler = this.CanExecuteChanged;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }
    }
}
