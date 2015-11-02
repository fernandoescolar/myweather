namespace MyWeather.Core.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Coordinates
    {
        [DataMember(Name = "lon")]
        public double Longitude { get; set; }

        [DataMember(Name = "lat")]
        public double Latitude { get; set; }
    }
}
