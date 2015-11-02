namespace MyWeather.Mvvm.Navigation
{
    using Base;
    using InversionOfControl;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Windows.UI.Core;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    internal sealed class NavigationService : INavigationService
    {
        private const string LastParameterPropertyName = "lastParameter";
        private const string LastParameterStackPropertyName = "lastParameterStack";
        private readonly Frame rootFrame;
        private readonly IContainer container;
        private Stack<object> lastParameterStack;
        private object lastParameter;
        private ViewInterceptor activeViewInterceptor;

        public NavigationService(Frame rootFrame, IContainer container)
        {
            this.rootFrame = rootFrame;
            this.container = container;
            this.lastParameterStack = new Stack<object>();
            this.lastParameterStack.Push(null);

            this.rootFrame.Navigated += this.OnNavigated;
            SystemNavigationManager.GetForCurrentView().BackRequested += this.OnBackPressed;
        }

        public bool CanGoBack()
        {
            return this.rootFrame.CanGoBack;
        }

        public bool CanGoForward()
        {
            return this.rootFrame.CanGoForward;
        }

        public void GoBack()
        {
            this.rootFrame.GoBack();
        }

        public void GoForward()
        {
            this.rootFrame.GoForward();
        }

        public void RemoveBackEntry()
        {
            this.lastParameterStack.Pop();
            this.rootFrame.BackStack.RemoveAt(0);
        }

        public Type GetPrevious()
        {
            var previousPage = this.rootFrame.BackStack.FirstOrDefault();
            return previousPage == null ? null : previousPage.SourcePageType;
        }

        public void Navigate(Type type, object parameter = null)
        {
            this.lastParameterStack.Push(this.lastParameter);
            this.lastParameter = parameter;

            this.rootFrame.Navigate(type);
        }

        public object GetState()
        {
            return new Dictionary<string, object> { 
                { LastParameterPropertyName, this.lastParameter }, 
                { LastParameterStackPropertyName, this.lastParameterStack.ToArray() }
            };
        }

        public void SetState(object state)
        {
            var dic = state as Dictionary<string, object>;
            if (dic != null)
            {
                if (dic.ContainsKey(LastParameterPropertyName))
                {
                    this.lastParameter = dic[LastParameterPropertyName];
                }

                if (dic.ContainsKey(LastParameterStackPropertyName))
                {
                    var objects = dic[LastParameterStackPropertyName] as IEnumerable<object>;
                    if (objects != null)
                    {
                        this.lastParameterStack = new Stack<object>(objects);
                    }
                }
            }
        }

        public void Clean()
        {
            if (this.activeViewInterceptor != null) this.activeViewInterceptor.Dispose();

            this.activeViewInterceptor = null;
        }

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            this.ManageLastParameter(e.NavigationMode);
            var nParameter = this.container.Resolve<NavigationParameter>();
            nParameter.Parameter = this.lastParameter;

            if (e.Content is IView)
            {
                if (this.activeViewInterceptor != null) this.activeViewInterceptor.Dispose();

                this.activeViewInterceptor = new ViewInterceptor(e.Content);
                this.activeViewInterceptor.LoadParameters(nParameter);
            }
        }

        private void ManageLastParameter(NavigationMode mode)
        {
            if (mode == NavigationMode.Back)
            {
                this.lastParameter = this.lastParameterStack.Pop();
            }

            if (mode == NavigationMode.Forward)
            {
                this.lastParameterStack.Push(this.lastParameter);
                this.lastParameter = null;
            }
        }

        private void OnBackPressed(object sender, BackRequestedEventArgs e)
        {
            if (this.CanGoBack())
            {
                e.Handled = true;
                this.GoBack();
            }
        }
    }
}
