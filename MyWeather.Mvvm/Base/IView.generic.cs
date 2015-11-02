namespace MyWeather.Mvvm.Base
{
    public interface IView<out TViewModel> : IView
    {
        TViewModel ViewModel { get; }
    }
}