﻿using HttpToBrokerAdapter.Interfaces;
using Newtonsoft.Json;
using System;

namespace HttpToBrokerAdapter.Models.Incl
{
    public class InclSensorMeas : ISensorMeas
    {
        public InclSensorMeas(int x, int y, int t, long ts, int x0 = -74, int y0 = 3)
        {
            X = GetAngDelta(x, x0);
            Y = GetAngDelta(y, y0);
            T = GetTemp(t);
            TS = ts;
        }
        [JsonProperty(PropertyName = "x")]
        public float X { get; set; }
        [JsonProperty(PropertyName = "y")]
        public float Y { get; set; }
        [JsonProperty(PropertyName = "temp")]
        public float T { get; set; }
        [JsonProperty(PropertyName = "t")]
        public long TS { get; set; }

        public static float GetAngDelta(int d, int d0)
        {
            return (float)Math.Round(Math.Asin((float)(d0 + 1024 - d) / 1638), 5);
        }

        private static float GetTemp(int temp)
        {
            return (float)Math.Round((temp - 197) / (-1.083), 5);
        }
    }
}