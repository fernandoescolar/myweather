namespace MyWeather.Mvvm.Serialization
{
    using Base;
    using Navigation;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Runtime.Serialization.Formatters;
    using Windows.UI.Xaml.Controls;

    internal class JsonStateManager : StateManager
    {
        public JsonStateManager(Frame rootFrame, INavigationService navigationService)
            : base(rootFrame, navigationService)
        {
        }

        protected override byte[] SerializeState(Dictionary<string, Dictionary<string, object>> states)
        {
            var serialized = JsonConvert.SerializeObject(states, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Full,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return System.Text.Encoding.UTF8.GetBytes(serialized);
        }

        protected override Dictionary<string, Dictionary<string, object>> DeserializeState(byte[] bytes)
        {
            var serialized = System.Text.Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            return JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(serialized, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Full,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }
    }
}
