namespace MyWeather.Mvvm.Navigation
{
    using Base;
    using Events;
    using InversionOfControl;

    internal sealed class NavigationParameter
    {
        public NavigationParameter(INavigationService navigationService, IViewModelLocator viewModelLocator, IEventAggregator eventAggregator, IViewModelStateManager viewModelStateManager)
        {
            this.NavigationService = navigationService;
            this.ViewModelLocator = viewModelLocator;
            this.EventAggregator = eventAggregator;
            this.ViewModelStateManager = viewModelStateManager;
        }

        public object Parameter { get; set; }

        public IViewModelLocator ViewModelLocator { get; private set; }

        public INavigationService NavigationService { get; private set; }

        public IEventAggregator EventAggregator { get; private set; }

        public IViewModelStateManager ViewModelStateManager { get; private set; }
    }
}
