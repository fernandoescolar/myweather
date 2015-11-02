namespace MyWeather.Mvvm.Commands
{
    using System;

    public class DelegateCommand : DelegateCommand<object>
    {
        public DelegateCommand(Action<object> action, Func<object, bool> canExecute) 
            : base(action, canExecute)
        {
        }

        public DelegateCommand(Action<object> action) 
            : base(action)
        {
        }
    }
}
