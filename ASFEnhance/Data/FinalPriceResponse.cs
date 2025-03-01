﻿using Newtonsoft.Json;
using SteamKit2;

namespace ASFEnhance.Data
{
    internal sealed class FinalPriceResponse
    {
        [JsonProperty(PropertyName = "success", Required = Required.Always)]
        public EResult Result { get; private set; }

        [JsonProperty(PropertyName = "base", Required = Required.DisallowNull)]
        public int BasePrice { get; private set; }

        [JsonProperty(PropertyName = "tax", Required = Required.DisallowNull)]
        public int Tax { get; private set; }

        [JsonProperty(PropertyName = "discount", Required = Required.DisallowNull)]
        public int Discount { get; private set; }
    }
}
