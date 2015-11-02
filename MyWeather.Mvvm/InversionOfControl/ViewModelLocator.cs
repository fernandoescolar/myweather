namespace MyWeather.Mvvm.InversionOfControl
{
    using MyWeather.Mvvm.Base;
    using System;

    internal class ViewModelLocator : IViewModelLocator
    {
        private readonly IContainer container;

        public ViewModelLocator(IContainer container)
        {
            this.container = container;
        }

        public TViewModel Get<TViewModel>() where TViewModel : IViewModel
        {
            return this.container.Resolve<TViewModel>();
        }

        public object Get(Type type)
        {
            return this.container.Resolve(type);
        }
    }
}
