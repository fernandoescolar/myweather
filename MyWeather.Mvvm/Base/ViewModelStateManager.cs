namespace MyWeather.Mvvm.Base
{
    using Exceptions;
    using Reactive;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal class ViewModelStateManager : IViewModelStateManager
    {
        private readonly IStateManager stateManager;

        public ViewModelStateManager(IStateManager stateManager)
        {
            this.stateManager = stateManager;
        }

        public void LoadViewModelState(IViewModel viewModel)
        {
            this.LoadViewModelState(viewModel, null);
        }

        public void SaveViewModelState(IViewModel viewModel)
        {
            this.SaveViewModelState(viewModel, null);
        }

        private void LoadViewModelState(IViewModel viewModel, IViewModel parent)
        {
            var viewModelKey = viewModel.GetType().FullName;
            if (parent != null)
            {
                viewModelKey = parent.GetType().FullName + "." + viewModelKey;
            }

            if (this.stateManager.States.ContainsKey(viewModelKey))
            {
                this.LoadRestorableStateProperties(viewModel, this.stateManager.States[viewModelKey]);
                viewModel.LoadState(this.stateManager.States[viewModelKey]);
            }
        }

        private void SaveViewModelState(IViewModel viewModel, IViewModel parent)
        {
            var viewModelKey = viewModel.GetType().FullName;
            if (parent != null)
            {
                viewModelKey = parent.GetType().FullName + "." + viewModelKey;
            }

            if (!this.stateManager.States.ContainsKey(viewModelKey))
            {
                this.stateManager.States.Add(viewModelKey, new Dictionary<string, object>());
            }

            this.SaveRestorableStateProperties(viewModel, this.stateManager.States[viewModelKey]);
            viewModel.SaveState(this.stateManager.States[viewModelKey]);
        }

        private void LoadRestorableStateProperties(IViewModel viewModel, Dictionary<string, object> state)
        {
            ForEachRestorableProperties(viewModel, property =>
               {
                   if (state.ContainsKey(property.Name))
                   {
                       if (property.PropertyType.GetTypeInfo().IsGenericType && property.PropertyType.GetTypeInfo().GetGenericTypeDefinition() == typeof(ReactiveProperty<>))
                       {
                           var reactiveProperty = property.GetValue(viewModel);
                           var valueProperty = reactiveProperty.GetType().GetTypeInfo().DeclaredProperties.FirstOrDefault(p => p.Name == "Value");
                           valueProperty.SetValue(reactiveProperty, state[property.Name]);
                       }
                       else if (property.PropertyType.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IViewModel)))
                       {
                           var childViewModel = property.GetValue(viewModel) as IViewModel;
                           this.LoadViewModelState(childViewModel, viewModel);
                       }
                       else
                       {
                           property.SetValue(viewModel, state[property.Name]);
                       }
                   }
               });
        }

        private void SaveRestorableStateProperties(IViewModel viewModel, Dictionary<string, object> state)
        {
            ForEachRestorableProperties(viewModel, property =>
                {
                    if (property.PropertyType.GetTypeInfo().IsGenericType && property.PropertyType.GetTypeInfo().GetGenericTypeDefinition() == typeof(ReactiveProperty<>))
                    {
                        var reactiveProperty = property.GetValue(viewModel);
                        var valueProperty = reactiveProperty.GetType().GetTypeInfo().DeclaredProperties.FirstOrDefault(p => p.Name == "Value");
                        state[property.Name] = valueProperty.GetValue(reactiveProperty);
                    }
                    else if (property.PropertyType.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IViewModel)))
                    {
                        var childViewModel = property.GetValue(viewModel) as IViewModel;
                        this.SaveViewModelState(childViewModel, viewModel);
                        state[property.Name] = true;
                    }
                    else
                    {
                        state[property.Name] = property.GetValue(viewModel);
                    }
                });
        }

        private static void ForEachRestorableProperties(object target, Action<PropertyInfo> action)
        {
            var properties = GetRestorableProperties(target);
            foreach (var property in properties)
            {
                try
                {
                    action(property);
                }
                catch (Exception ex)
                {
                    throw new RestorePropertyException(property.Name, ex);
                }
            }
        }

        private static IEnumerable<PropertyInfo> GetRestorableProperties(object obj)
        {
            return obj.GetType().GetTypeInfo().DeclaredProperties.Where(p => p.GetCustomAttributes(typeof(RestorableStateAttribute), false).Any());
        }
    }
}
