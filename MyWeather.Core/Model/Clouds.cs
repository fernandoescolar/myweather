namespace MyWeather.Core.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Clouds
    {
        [DataMember(Name = "all")]
        public int All { get; set; }
    }
}
