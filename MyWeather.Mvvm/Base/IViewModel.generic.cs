namespace MyWeather.Mvvm.Base
{
    public interface IViewModel<out TModel> : IViewModel
    {
        TModel Model { get; }
    }
}
