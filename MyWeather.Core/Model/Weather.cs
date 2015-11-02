namespace MyWeather.Core.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Weather
    {

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "main")]
        public string Main { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "icon")]
        public string Icon { get; set; }
    }
}
