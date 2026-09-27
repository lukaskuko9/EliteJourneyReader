using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EliteJourneyReader.EventMessages
{
    public sealed class MarketEventMessage : JourneyEventMessage
    {

        [JsonProperty("MarketID")]
        public ulong MarketId { get; set; }
        public string StationName { get; set; } = string.Empty;
        public string StationType { get; set; } = string.Empty;
        public string StarSystem { get; set; } = string.Empty;

        public override string EventTypeName => "Market";
    }
}
