namespace MyWeather.Core.Model
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class WeatherInfo
    {
        [DataMember(Name = "city")]
        public City City { get; set; }

        [DataMember(Name = "cod")]
        public string Cod { get; set; }

        [DataMember(Name = "message")]
        public double Message { get; set; }

        [DataMember(Name = "cnt")]
        public int Cnt { get; set; }

        [DataMember(Name = "list")]
        public IList<WeatherResult> Results { get; set; }
    }
}
