namespace MyWeather.Core.Model
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Temperature
    {

        [DataMember(Name = "day")]
        public double Day { get; set; }

        [DataMember(Name = "min")]
        public double Min { get; set; }

        [DataMember(Name = "max")]
        public double Max { get; set; }

        [DataMember(Name = "night")]
        public double Night { get; set; }

        [DataMember(Name = "eve")]
        public double Evening { get; set; }

        [DataMember(Name = "morn")]
        public double Morning { get; set; }
    }

}
