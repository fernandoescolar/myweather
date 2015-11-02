namespace MyWeather.Mvvm.Navigation
{
    using System;

    public interface INavigationService
    {
        bool CanGoBack();
        bool CanGoForward();
        void GoBack();
        void GoForward();
        void RemoveBackEntry();
        Type GetPrevious();
        void Navigate(Type type, object parameter = null);
        object GetState();
        void SetState(object state);
        void Clean();
    }
}
