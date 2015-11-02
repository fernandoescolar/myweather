namespace MyWeather.Core.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Wind
    {
        [DataMember(Name = "speed")]
        public double Speed { get; set; }

        [DataMember(Name = "deg")]
        public double Deg { get; set; }
    }
}
