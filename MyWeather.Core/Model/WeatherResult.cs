namespace MyWeather.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class WeatherResult
    {

        [DataMember(Name = "dt")]
        public long Dt { get; set; }

        [DataMember(Name = "temp")]
        public Temperature Temperature { get; set; }

        [DataMember(Name = "pressure")]
        public double Pressure { get; set; }

        [DataMember(Name = "humidity")]
        public int Humidity { get; set; }

        [DataMember(Name = "weather")]
        public IList<Weather> Weather { get; set; }

        [DataMember(Name = "speed")]
        public double Speed { get; set; }

        [DataMember(Name = "deg")]
        public int Deg { get; set; }

        [DataMember(Name = "clouds")]
        public int Clouds { get; set; }

        [DataMember(Name = "rain")]
        public double Rain { get; set; }

        public DateTime DateTime
        {
            get
            {
                DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return start.AddMilliseconds(this.Dt * 1000).ToLocalTime();
            }
        }
       
    }
}
