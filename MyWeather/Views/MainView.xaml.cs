namespace MyWeather.Views
{
    using Core.ViewModels;
    using Mvvm.Base;
    using Mvvm.Events;
    using Windows.UI.Xaml.Controls;

    public sealed partial class MainView : Page, IView<IMainViewModel>
    {
        public MainView()
        {
            this.InitializeComponent();
        }

        public IEventAggregator EventAggregator { get; set; }

        public IMainViewModel ViewModel { get { return this.DataContext as IMainViewModel; } }
    }
}
