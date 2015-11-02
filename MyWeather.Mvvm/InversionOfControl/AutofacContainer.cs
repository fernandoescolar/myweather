namespace MyWeather.Mvvm.InversionOfControl
{
    using System;
    using Autofac;

    internal sealed class AutofacContainer : IContainer
    {
        private readonly Autofac.IContainer innerContainer;

        public AutofacContainer(Func<Autofac.IContainer> innerContainer)
        {
            this.innerContainer = innerContainer();
        }

        public object Resolve(Type type)
        {
            return this.innerContainer.Resolve(type);
        }

        public object Resolve(Type type, string name)
        {
            return this.innerContainer.ResolveNamed(name, type);
        }

        public TInterface Resolve<TInterface>()
        {
            return this.innerContainer.Resolve<TInterface>();
        }

        public TInterface Resolve<TInterface>(string name)
        {
            return this.innerContainer.ResolveNamed<TInterface>(name);
        }
    }
}
