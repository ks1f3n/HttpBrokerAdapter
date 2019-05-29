using HttpToBrokerAdapter.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HttpToBrokerAdapter.Models
{
    public class Message
    {
        public Message(Sensor sensor)
        {
            Sensor = sensor;
        }
        public Message(SensorInfo info, IEnumerable<ISensorMeas> meas) :this(new Sensor(info, meas))
        {            
        }
        [JsonProperty(PropertyName = "sensor")]
        public Sensor Sensor { get; set; }
    }
}
