namespace MyWeather.Mvvm.Base
{
    using InversionOfControl;
    using Navigation;
    using System;
    using System.Linq;
    using System.Reflection;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    internal sealed class ViewInterceptor : IDisposable
    {
        private readonly Page page;
        private readonly IView view;
        private readonly Type interfaceType;
        private readonly bool isGeneric;
        private bool disposed;
        private IViewModelLocator ViewModelLocator { get; set; }
        private NavigationParameter navigationParameter;
       
        public ViewInterceptor(object view)
        {
            if (!(view is Page) || !(view is IView))
            {
                throw new ArgumentException("view");
            }

            this.page = view as Page;
            this.view = view as IView;
            this.page.Loaded += OnLoaded;
            this.interfaceType = view.GetType().GetTypeInfo().ImplementedInterfaces.FirstOrDefault(i => i.Name == typeof (IView<>).Name);
            this.isGeneric = this.interfaceType != null;
        }

        public void LoadParameters(NavigationParameter navigationParameter)
        {
            this.view.EventAggregator = navigationParameter.EventAggregator;
            this.ViewModelLocator = navigationParameter.ViewModelLocator;

            if (this.isGeneric)
            {
                this.navigationParameter = navigationParameter;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (disposing && this.isGeneric && !this.disposed)
            {
                var viewModel = this.page.DataContext as IViewModel;
                if (viewModel != null)
                {
                    this.navigationParameter.ViewModelStateManager.SaveViewModelState(viewModel);
                }
            }

            this.disposed = true;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            this.page.Loaded -= OnLoaded;

            if (this.isGeneric)
            {
                var dataContext = this.ViewModelLocator.Get(this.interfaceType.GenericTypeArguments[0]);
                var viewModel = dataContext as IViewModel;

                if (viewModel != null)
                {
                    if (viewModel is ViewModel)
                    {
                        ((ViewModel)viewModel).LoadNavigationParameter(this.navigationParameter);
                    }

                    this.navigationParameter.ViewModelStateManager.LoadViewModelState(viewModel);
                    ((IViewModel)viewModel).Load(this.navigationParameter.Parameter);
                }

                this.page.DataContext = dataContext;
            }
        }
    }
}
