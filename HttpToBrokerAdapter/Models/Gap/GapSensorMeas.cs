using HttpToBrokerAdapter.Interfaces;
using Newtonsoft.Json;

namespace HttpToBrokerAdapter.Models.Gap
{
    public class GapSensorMeas : ISensorMeas
    {
        public GapSensorMeas(float d, long t)
        {
            D = d;
            T = t;
        }
        // херня
        public GapSensorMeas(string d, string t)
        {
            D = float.Parse(d.Trim('"'));
            T = long.Parse(t.Trim('"'));
        }
        [JsonProperty(PropertyName = "d")]
        public float D { get; set; }
        [JsonProperty(PropertyName = "t")]
        public long T { get; set; }
    }
}
