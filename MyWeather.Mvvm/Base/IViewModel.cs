namespace MyWeather.Mvvm.Base
{
    using Events;
    using Navigation;
    using System.Collections.Generic;
    using System.ComponentModel;

    public interface IViewModel : INotifyPropertyChanged
    {
        event PropertyChangedEventHandler PropertyChanged;
        INavigationService NavigationService { get; }
        IEventAggregator EventAggregator { get; }
        void LoadState(Dictionary<string, object> state);
        void SaveState(Dictionary<string, object> state);
        void Load(object parameter);
    }
}
