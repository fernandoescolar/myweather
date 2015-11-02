namespace MyWeather.Mvvm.Configuration
{
    using Autofac;
    using Base;
    using Events;
    using InversionOfControl;
    using Navigation;
    using Serialization;
    using Windows.UI.Xaml.Controls;
    using IContainer = InversionOfControl.IContainer;

    public class MvvmModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            this.RegisterCommonArtifacts(builder);

            base.Load(builder);
        }

        private void RegisterCommonArtifacts(ContainerBuilder builder)
        {
            builder.RegisterType<AutofacContainer>().As<IContainer>().SingleInstance();
            builder.Register(c => new NavigationService(c.ResolveNamed<Frame>("Root"), c.Resolve<IContainer>())).As<INavigationService>().SingleInstance();
            builder.Register(c => new JsonStateManager(c.ResolveNamed<Frame>("Root"), c.Resolve<INavigationService>())).As<IStateManager>().SingleInstance();
            builder.RegisterType<ViewModelStateManager>().As<IViewModelStateManager>().SingleInstance();
            builder.RegisterType<ReactiveEventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<ViewModelLocator>().As<IViewModelLocator>().SingleInstance();
            builder.RegisterType<NavigationParameter>();
        }
    }
}
