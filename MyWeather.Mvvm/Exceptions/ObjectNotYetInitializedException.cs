namespace MyWeather.Mvvm.Exceptions
{
    using System;

    public class ObjectNotYetInitializedException : Exception
    {
        public const string ObjectNotYetInitializedMessage = "The object has not been yet initialized. Please, don't use it in the constructor. You can use it the first time in the 'Load' method.";

        public ObjectNotYetInitializedException(string message)
            : base(message)
        {
        }

        public ObjectNotYetInitializedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
