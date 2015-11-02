namespace MyWeather.Core.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class City
    {

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "coord")]
        public Coordinates Coordinates { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "population")]
        public int Population { get; set; }
    }

}
