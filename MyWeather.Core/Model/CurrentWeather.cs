namespace MyWeather.Core.Model
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;


    [DataContract]
    public class CurrentWeather
    {

        [DataMember(Name = "coord")]
        public Coordinates Coordinates { get; set; }

        [DataMember(Name = "weather")]
        public IList<Weather> Weather { get; set; }

        [DataMember(Name = "base")]
        public string Base { get; set; }

        [DataMember(Name = "main")]
        public MainInfo MainInfo { get; set; }

        [DataMember(Name = "wind")]
        public Wind Wind { get; set; }

        [DataMember(Name = "clouds")]
        public Clouds Clouds { get; set; }

        [DataMember(Name = "dt")]
        public int Dt { get; set; }

        [DataMember(Name = "sys")]
        public SysInfo SysInfo { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "cod")]
        public int Cod { get; set; }
    }
}
