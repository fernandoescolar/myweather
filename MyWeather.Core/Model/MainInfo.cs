namespace MyWeather.Core.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class MainInfo
    {
        [DataMember(Name = "temp")]
        public double Temperature { get; set; }

        [DataMember(Name = "pressure")]
        public double Pressure { get; set; }

        [DataMember(Name = "humidity")]
        public double Humidity { get; set; }

        [DataMember(Name = "temp_min")]
        public double TemperatureMin { get; set; }

        [DataMember(Name = "temp_max")]
        public double TemperatureMax { get; set; }
    }
}
