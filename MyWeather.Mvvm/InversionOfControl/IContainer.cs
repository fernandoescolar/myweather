namespace MyWeather.Mvvm.InversionOfControl
{
    using System;

    public interface IContainer
    {
        object Resolve(Type type);
        object Resolve(Type type, string name);

        TInterface Resolve<TInterface>();
        TInterface Resolve<TInterface>(string name);
    }
}
