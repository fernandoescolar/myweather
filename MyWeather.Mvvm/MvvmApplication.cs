namespace MyWeather.Mvvm
{
    using Autofac;
    using Base;
    using Configuration;
    using Navigation;
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Threading.Tasks;
    using Windows.ApplicationModel;
    using Windows.ApplicationModel.Activation;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Animation;
    using Windows.UI.Xaml.Navigation;
    using IContainer = InversionOfControl.IContainer;

    public abstract class MvvmApplication : Application
    {
        private readonly ContainerBuilder builder;
        private IContainer container;
        private bool phoneApplicationInitialized;
        private TransitionCollection transitions;

        protected MvvmApplication()
        {
            this.builder = new Autofac.ContainerBuilder();
            this.UnhandledException += this.OnApplicationUnhandledException;
            this.Suspending += this.OnApplicationSuspending;
        }

        protected Frame RootFrame { get; private set; }

        protected INavigationService NavigationService { get; private set; }

        protected IStateManager StateManager { get; private set; }

        protected abstract void SetUp(Frame rootFrame);

        protected virtual Task OnStartingAsync(IContainer container)
        {
            return Task.Factory.StartNew(() => { });
        }

        protected virtual Task OnClosingAsync(IContainer container)
        {
            return Task.Factory.StartNew(() => { });
        }

        protected abstract void OnLoaded();

        protected void RegisterAssembly(params string[] assemblyNames)
        {
            foreach (var assemblyPath in assemblyNames)
            {
                var assemblyName = new AssemblyName(assemblyPath);
                var assembly = Assembly.Load(assemblyName);
                this.builder.RegisterAssemblyModules(assembly);
            }
        }

        protected void RegisterView<TView>() where TView : IView
        {
            this.builder.RegisterType<TView>().Named<IView>(typeof(TView).FullName);
        }

        protected void RegisterViewModel<TIViewModel, TViewModel>(bool keepAlive = false)
            where TViewModel : TIViewModel
            where TIViewModel : IViewModel
        {
            if (keepAlive)
            {
                this.builder.RegisterType<TViewModel>().As<TIViewModel>().SingleInstance();
            }
            else
            {
                this.builder.RegisterType<TViewModel>().As<TIViewModel>();
            }
        }

        protected void RegisterViewModel<TViewModel>(bool keepAlive = false)
            where TViewModel : IViewModel
        {
            if (keepAlive)
            {
                this.builder.RegisterType<TViewModel>().SingleInstance();
            }
            else
            {
                this.builder.RegisterType<TViewModel>();
            }
        }

        protected void Register<TInterface, TClass>(bool singleinstance = false)
            where TClass : TInterface
        {
            if (singleinstance)
            {
                this.builder.RegisterType<TClass>().As<TInterface>().SingleInstance();
            }
            else
            {
                this.builder.RegisterType<TClass>().As<TInterface>();
            }
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            if (phoneApplicationInitialized)
                return;

            this.InitializeRootFrame();
            await this.BootstrapAsync();
            this.InitializeNavigationService();
            await this.InitializeStateManager(e.PreviousExecutionState);
            this.PreventTransitionsOnStartup();
            this.OnLoaded();

            Window.Current.Activate();
            phoneApplicationInitialized = true;
        }

        private void InitializeRootFrame()
        {
            this.RootFrame = new Frame();
            this.RootFrame.CacheSize = 1;
            this.RootFrame.NavigationFailed += OnRootFrameNavigationFailed;

            Window.Current.Content = this.RootFrame;

            if (Window.Current.Content != this.RootFrame)
                Window.Current.Content = this.RootFrame;

            Window.Current.Activate();
        }

        private void PreventTransitionsOnStartup()
        {
            if (this.RootFrame.Content == null)
            {
                if (this.RootFrame.ContentTransitions != null)
                {
                    this.transitions = new TransitionCollection();
                    foreach (var c in this.RootFrame.ContentTransitions)
                    {
                        this.transitions.Add(c);
                    }
                }

                this.RootFrame.Navigated += this.RestoreTransitions;
                this.RootFrame.ContentTransitions = null;
            }
        }

        private void InitializeNavigationService()
        {
            this.NavigationService = this.container.Resolve<INavigationService>();
        }

        private async Task InitializeStateManager(ApplicationExecutionState previousState)
        {
            this.StateManager = this.container.Resolve<IStateManager>();
            if (previousState == ApplicationExecutionState.Terminated)
            {
                await this.StateManager.RestoreAsync();
            }
        }

        private void RestoreTransitions(object sender, NavigationEventArgs e)
        {
            this.RootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            this.RootFrame.Navigated -= RestoreTransitions;
        }

        private async Task BootstrapAsync()
        {
            this.builder.Register(c => this.RootFrame).Named<Frame>("Root");
            this.builder.RegisterModule<MvvmModule>();

            this.SetUp(this.RootFrame);

            // HACK: Register the current container
            Autofac.IContainer ioc = null;
            Func<Autofac.IContainer> factory = () => ioc;
            this.builder.RegisterInstance(factory);
            ////////////////////////////////////////////////
            ioc = this.builder.Build();
            this.container = ioc.Resolve<IContainer>();

            await this.OnStartingAsync(this.container);
        }

        private async void OnApplicationSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            this.NavigationService.Clean();
            await this.StateManager.SaveAsync();
            await this.OnClosingAsync(this.container);
            deferral.Complete();
        }

        private void OnRootFrameNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                Debugger.Break();
            }
        }

        private void OnApplicationUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                Debugger.Break();
            }
        }
    }
}
