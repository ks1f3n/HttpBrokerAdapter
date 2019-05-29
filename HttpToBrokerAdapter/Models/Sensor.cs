﻿using HttpToBrokerAdapter.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace HttpToBrokerAdapter.Models
{
    public class Sensor
    {
        public Sensor(SensorInfo info, IEnumerable<ISensorMeas> meas)
        {
            Info = info;
            Meas = meas;
        }
        [JsonProperty(PropertyName = "info")]
        public SensorInfo Info { get; set; }
        [JsonProperty(PropertyName = "meas")]
        public IEnumerable<ISensorMeas> Meas { get; set; }
    }
}
