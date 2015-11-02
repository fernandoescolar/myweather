namespace MyWeather.Mvvm.InversionOfControl
{
    using System;
    using MyWeather.Mvvm.Base;

    internal interface IViewModelLocator
    {
        TViewModel Get<TViewModel>() where TViewModel : IViewModel;
        object Get(Type type);
    }
}
