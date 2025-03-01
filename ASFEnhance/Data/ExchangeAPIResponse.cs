﻿using Newtonsoft.Json;

namespace ASFEnhance.Data
{
    internal class ExchangeAPIResponse
    {
        [JsonProperty(PropertyName = "base", Required = Required.Always)]
        public string Base { get; set; }

        [JsonProperty(PropertyName = "date", Required = Required.Always)]
        public string Date { get; set; }

        [JsonProperty(PropertyName = "time_last_updated", Required = Required.Always)]
        public long UpdateTime { get; set; }

        [JsonProperty(PropertyName = "rates", Required = Required.Always)]
        public Dictionary<string, double> Rates { get; set; }
    }
}
