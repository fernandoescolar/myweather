namespace MyWeather.Core.Model
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class SysInfo
    {

        [DataMember(Name = "type")]
        public int Type { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "message")]
        public double Message { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "sunrise")]
        public int Sunrise { get; set; }

        [DataMember(Name = "sunset")]
        public int Sunset { get; set; }

        public DateTime SunriseDateTime
        {
            get
            {
                DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return start.AddMilliseconds(this.Sunrise * 1000).ToLocalTime();
            }
        }

        public DateTime SunsetDateTime
        {
            get
            {
                DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return start.AddMilliseconds(this.Sunset * 1000).ToLocalTime();
            }
        }
    }
}
