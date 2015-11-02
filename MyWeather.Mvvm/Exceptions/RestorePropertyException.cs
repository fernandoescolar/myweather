namespace MyWeather.Mvvm.Exceptions
{
    using System;

    public class RestorePropertyException: Exception
    {
        public const string RestorePropertyExceptionMessage = "The property {0} could not been restored.";

        public RestorePropertyException(string property)
            : base(string.Format(RestorePropertyExceptionMessage, property))
        {
        }

        public RestorePropertyException(string property, Exception innerException)
            : base(string.Format(RestorePropertyExceptionMessage, property), innerException)
        {
        }
    }
}
