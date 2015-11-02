namespace MyWeather.Mvvm.Base
{
    public abstract class ViewModel<TModel> : ViewModel, IViewModel<TModel>
    {
        private TModel model;

        protected ViewModel()
        {
        }

        protected ViewModel(TModel model)
        {
            this.Model = model;
        }

        public TModel Model
        {
            get { return this.model; }
            set
            {
                if (this.model == null || !this.model.Equals(value))
                {
                    this.model = value;
                    this.OnPropertyChanged("Model");
                }
            }
        }
    }
}
