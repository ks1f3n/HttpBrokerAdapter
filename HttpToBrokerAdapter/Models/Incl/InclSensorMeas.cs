using HttpToBrokerAdapter.Interfaces;
using Newtonsoft.Json;

namespace HttpToBrokerAdapter.Models.Incl
{
    public class InclSensorMeas : ISensorMeas
    {
        public InclSensorMeas(float x, float y, float t, long ts)
        {
            X = x;
            Y = y;
            T = t;
            TS = ts;
        }
        // херня
        public InclSensorMeas(string x, string y, string t, string ts)
        {
            X = float.Parse(x.Trim('"'));
            Y = float.Parse(y.Trim('"'));
            T = float.Parse(t.Trim('"'));
            TS = long.Parse(ts.Trim('"'));
        }
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
        [JsonProperty(PropertyName = "temp")]
        public float T { get; set; }
        [JsonProperty(PropertyName = "t")]
        public long TS { get; set; }
    }
}