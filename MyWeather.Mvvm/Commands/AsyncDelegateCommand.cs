namespace MyWeather.Mvvm.Commands
{
    using System;
    using System.Threading.Tasks;

    public class AsyncDelegateCommand : AsyncDelegateCommand<object>
    {
        public AsyncDelegateCommand(Func<object, Task> execute) : 
            base(execute)
        {
        }

        public AsyncDelegateCommand(Func<object, Task> executeAction, Predicate<object> canExecuteAction) : 
            base(executeAction, canExecuteAction)
        {
        }
    }
}
