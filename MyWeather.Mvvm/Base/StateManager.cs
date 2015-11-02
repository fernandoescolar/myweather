namespace MyWeather.Mvvm.Base
{
    using Navigation;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Windows.Security.Cryptography.DataProtection;
    using Windows.Storage;
    using Windows.UI.Xaml.Controls;

    internal abstract class StateManager : IStateManager
    {
        private const string SessionStateFileName = "_application.state";
        private const string RootFrameStatePropertyName = "_rootFrame_";
        private const string NavigationStatePropertyName = "_navigation_";
        private readonly Frame rootFrame;
        private INavigationService navigationService;
        private readonly Dictionary<string, Dictionary<string, object>> states;

        public Dictionary<string, Dictionary<string, object>> States { get { return this.states; } }

        public StateManager(Frame rootFrame, INavigationService navigationService)
        {
            this.rootFrame = rootFrame;
            this.navigationService = navigationService;
            this.states = new Dictionary<string, Dictionary<string, object>>();
        }

        public async Task SaveAsync()
        {
            this.SaveApplicationState();
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(SessionStateFileName, CreationCollisionOption.ReplaceExisting);
            var bytes = this.SerializeState(this.states);
            using (var sessionData = new MemoryStream(bytes))
            {
                using (var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    sessionData.Seek(0, SeekOrigin.Begin);
                    var provider = new DataProtectionProvider("LOCAL=user");

                    await provider.ProtectStreamAsync(sessionData.AsInputStream(), fileStream);
                    await fileStream.FlushAsync();
                }
            }
        }

        public async Task RestoreAsync()
        {
            var file = await ApplicationData.Current.LocalFolder.GetFileAsync(SessionStateFileName);
            using (var inStream = await file.OpenSequentialReadAsync())
            {
                using (var memoryStream = new MemoryStream())
                {
                    var provider = new DataProtectionProvider("LOCAL=user");
                    await provider.UnprotectStreamAsync(inStream, memoryStream.AsOutputStream());
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    var bytes = memoryStream.ToArray();
                    this.DeserializeState(bytes);
                }
            }

            this.LoadApplicationState();
        }

        protected abstract byte[] SerializeState(Dictionary<string, Dictionary<string, object>> states);

        protected abstract Dictionary<string, Dictionary<string, object>> DeserializeState(byte[] bytes);

        private void SaveApplicationState()
        {
            this.states[RootFrameStatePropertyName] = new Dictionary<string, object> { { RootFrameStatePropertyName, this.rootFrame.GetNavigationState() } };
            this.states[NavigationStatePropertyName] = new Dictionary<string, object> { { NavigationStatePropertyName, this.navigationService.GetState() } };
        }

        private void LoadApplicationState()
        {
            if (this.states.ContainsKey(RootFrameStatePropertyName) && this.states[RootFrameStatePropertyName].ContainsKey(RootFrameStatePropertyName))
            {
                this.rootFrame.SetNavigationState(this.states[RootFrameStatePropertyName][RootFrameStatePropertyName] as string);
            }

            if (this.states.ContainsKey(NavigationStatePropertyName) && this.states[NavigationStatePropertyName].ContainsKey(NavigationStatePropertyName))
            {
                this.navigationService.SetState(this.states[NavigationStatePropertyName][NavigationStatePropertyName] as string);
            }
        }
    }
}
