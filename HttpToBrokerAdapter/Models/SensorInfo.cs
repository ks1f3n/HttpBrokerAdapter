using Newtonsoft.Json;

namespace HttpToBrokerAdapter.Models
{
    public class SensorInfo
    {
        public SensorInfo(int per, float volt, int csq)
        {
            Per = per;
            Volt = volt;
            Csq = csq;
        }
        [JsonProperty(PropertyName = "per")]
        public int Per { get; set; }
        [JsonProperty(PropertyName = "volt")]
        public float Volt { get; set; }
        [JsonProperty(PropertyName = "csq")]
        public int Csq { get; set; }
    }
}
