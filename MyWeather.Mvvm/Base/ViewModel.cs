namespace MyWeather.Mvvm.Base
{
    using Events;
    using Exceptions;
    using Navigation;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public abstract class ViewModel : NotifyPropertyChangeObject, IViewModel
    {
        private INavigationService navigationService;
        private IEventAggregator eventAggregator;

        public INavigationService NavigationService
        {
            get
            {
                if (this.navigationService == null)
                    throw new ObjectNotYetInitializedException(ObjectNotYetInitializedException.ObjectNotYetInitializedMessage);

                return this.navigationService;
            }
            private set { this.navigationService = value; }
        }

        public IEventAggregator EventAggregator
        {
            get
            {
                if (this.eventAggregator == null)
                    throw new ObjectNotYetInitializedException(ObjectNotYetInitializedException.ObjectNotYetInitializedMessage);

                return this.eventAggregator;
            }
            private set { this.eventAggregator = value; }
        }

        public void LoadState(Dictionary<string, object> state)
        {
            this.OnLoadState(state);
        }

        public void SaveState(Dictionary<string, object> state)
        {
            this.OnSaveState(state);
        }

        public void Load(object parameter)
        {
            this.OnLoad(parameter);
            ForEachChildViewModels(this, vm => vm.Load(parameter));
        }

        protected virtual void OnLoadState(Dictionary<string, object> state)
        {
        }

        protected virtual void OnSaveState(Dictionary<string, object> state)
        {
        }

        protected virtual void OnLoad(object parameter)
        {
        }

        internal void LoadNavigationParameter(NavigationParameter parameter)
        {
            this.NavigationService = parameter.NavigationService;
            this.EventAggregator = parameter.EventAggregator;
            this.LoadChildsNavigationParameter(parameter);
        }

        private void LoadChildsNavigationParameter(NavigationParameter parameter)
        {
            ForEachChildViewModels(this, vm => {
                var viewModel = vm as ViewModel;
                if (viewModel != null)
                {
                    viewModel.LoadNavigationParameter(parameter);
                }
            });
        }

        private static void ForEachChildViewModels(object obj, Action<IViewModel> action)
        {
            var childs = GetChildViewModelProperties(obj);
            foreach (var property in childs)
            {
                var child = property.GetValue(obj) as IViewModel;
                if (child != null)
                {
                    action(child);
                }
            }
        }

        private static IEnumerable<PropertyInfo> GetChildViewModelProperties(object obj)
        {
            return obj.GetType().GetTypeInfo().DeclaredProperties.Where(p => p.PropertyType.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IViewModel)));
        }
    }
}
