namespace MyWeather.Mvvm.Base
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IStateManager
    {
        Dictionary<string, Dictionary<string, object>> States { get; }

        Task SaveAsync();

        Task RestoreAsync();
    }
}
