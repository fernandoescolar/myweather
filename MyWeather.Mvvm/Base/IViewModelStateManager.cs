namespace MyWeather.Mvvm.Base
{
    internal interface IViewModelStateManager
    {
        void LoadViewModelState(IViewModel viewModel);

        void SaveViewModelState(IViewModel viewModel);
    }
}
